using CraigsListClone.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace CraigsListClone.Controllers
{
    public class PostController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Posts
        public ViewResult Index()
        {
            return View(db.Posts
                .OrderByDescending(q => q.Created)
                .ToList()
                .Take(5));
        }

        // CREATE: Initial View
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.controlName = "Post";
            return View();
        }

        // CREATE: Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Desc,Cost,OwnerId")] Post post)
        {
            if (ModelState.IsValid)
            {
                var OwnerId = User.Identity.GetUserId();

                post.Created = DateTime.Now;
                post.OwnerId = OwnerId;

                post.CityId = (int)db.Users
                    .Where(u => u.Id == OwnerId)
                    .Select(u => u.CityId)
                    .SingleOrDefault();
                    
                db.Posts.Add(post);

                db.SaveChanges();
                RedirectToAction("Index", "Home");
            }
            ModelState.Clear();
            return View();
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

            return View(post);
        }

        // EDIT: Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Desc,Cost,OwnerId")] Post post)
        {
            if (ModelState.IsValid)
            {
                post.OwnerId = User.Identity.GetUserId();
                post.Created = post.Created;
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
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

            return RedirectToAction("Index");
        }
    }
}

