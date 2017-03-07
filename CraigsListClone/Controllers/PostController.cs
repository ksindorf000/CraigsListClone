using CraigsListClone.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CraigsListClone.Controllers
{
    public class PostController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Posts
        public ViewResult Index()
        {
            List<PostViewModel> pvmList = new List<PostViewModel>();
            List<Post> postList = db.Posts.ToList();
            foreach (var post in postList)
            {
                PostViewModel pVM = new PostViewModel(post);
                pvmList.Add(pVM);
            };
            return View(pvmList);
        }

        // CREATE: Initial View
        [Authorize]
        public ActionResult Create()
        {
            var catList = db.Categories.ToList();
            var model = new PostViewModel
            {
                Categories = GetCatList()
            };
            return View(model);
        }

        /* https://github.com/NLHawkins/ShopList/blob/master/ShopList/Controllers/PostController.cs */
        private IEnumerable<SelectListItem> GetCatList()
        {
            var catList = db.Categories
                .Where(c => c.ParentId != null)
                .Select(c => new SelectListItem
                        {
                            Value = c.Id.ToString(),
                            Text = c.Name
                        });

            return new SelectList(catList, "Value", "Text");
        }

        // CREATE: Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Desc,Cost,OwnerId")] Post post)
        {
            if (!ModelState.IsValid)
            {
                ModelState.Clear();
                return View();
            }

            var OwnerId = User.Identity.GetUserId();

            post.Created = DateTime.Now;
            post.OwnerId = OwnerId;

            post.CityId = (int)db.Users
                .Where(u => u.Id == OwnerId)
                .Select(u => u.CityId)
                .SingleOrDefault();

            db.Posts.Add(post);

            var postCat = new PostCategory()
            {
                PostId = post.Id,
                CatId = int.Parse(Request.Form["CatId"])
            };

            db.PostCategories.Add(postCat);


            db.SaveChanges();


            return RedirectToAction("Index", "Home");

        }

        // POST: Image Upload
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(UploadViewModel formData, int? id)
        {
            //Get File and Create Path
            var uploadedFile = Request.Files[0];
            string filename = $"{DateTime.Now.Ticks}{uploadedFile.FileName}";
            var serverPath = Server.MapPath(@"~\Upload");
            var fullPath = Path.Combine(serverPath, filename);

            //Save Image
            uploadedFile.SaveAs(fullPath);

            var userId = User.Identity.GetUserId();
            int pId;

            //Get Question Id if not provided            
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                pId = (int)id;
            }

            //Create Upload Entry
            var uploadModel = new Upload
            {
                Caption = formData.Caption,
                File = filename,
                OwnerId = User.Identity.GetUserId(),
                RefId = pId,
                TypeRef = "Post",
            };

            db.Uploads.Add(uploadModel);
            db.SaveChanges();

            return RedirectToAction("Detail", pId);

        }

        // DETAIL
        [Route("p/{id}")]
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Post post = db.Posts.Find(id);

            if (post == null)
            {
                return HttpNotFound();
            }

            ViewBag.UploadsList = db.Uploads
                .Where(u => u.TypeRef == "Post" && u.RefId == post.Id)
                .ToList();

            if (User.Identity.GetUserId() == post.OwnerId)
            {
                ViewBag.CanEdit = true;
            }
            else
            {
                ViewBag.CanEdit = false;
            }

            return View(post);
        }

        // EDIT: Initial View
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string userId = User.Identity.GetUserId();
            Post post = db.Posts
                .Where(q => q.Id == id
                && q.OwnerId == userId)
                .FirstOrDefault();

            if (post == null)
            {
                return HttpNotFound();
            }

            ViewBag.CityId = new SelectList(db.Cities, "Id", "Name");

            return View(post);
        }

        // EDIT: Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Desc,Cost,CityId,OwnerId")] Post post)
        {
            if (ModelState.IsValid)
            {
                post.OwnerId = User.Identity.GetUserId();
                post.Created = post.Created;
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // DELETE: Initial View
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string userId = User.Identity.GetUserId();
            Post post = db.Posts
                .Where(p => p.Id == id
                && p.OwnerId == userId)
                .FirstOrDefault();

            if (post == null)
            {
                return HttpNotFound();
            }

            return View(post);
        }

        // DELETE: Post
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}

