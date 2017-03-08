using CraigsListClone.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CraigsListClone.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Posts
        public ViewResult Index()
        {
            ViewBag.Cities = db.Cities.ToList();
            ViewBag.Cats = db.Categories
                .Where(c => c.ParentId != null)
                .ToList();
            List<Post> postList = db.Posts.OrderByDescending(p => p.Created).ToList();

            CategorizePosts(postList);

            return View();
        }

        //Sort Posts By City
        public ActionResult CitySort(int id)
        {
            var city = db.Cities.Find(id);
            ViewBag.City = city.Name + ", " + city.State;

            ViewBag.PostVMList = new List<PostViewModel>();
            List<Post> postList = db.Posts
                .Where(p => p.CityId == id)
                .OrderByDescending(p => p.Created)
                .ToList();

            CategorizePosts(postList);

            return View();
        }


        //Sort Posts By Category
        public ActionResult CategorySort(int? id, string sortBy)
        {
            var category = db.Categories.Find(id);
            var parent = db.Categories.Find(category.ParentId);
            ViewBag.Cat = category.Name + ", " + parent.Name;

            ViewBag.Posts = new List<PostViewModel>();

            List<int> postIdList = db.PostCategories
                .Where(pc => pc.CatId == id)
                .Select(pc => pc.PostId).ToList();

            ViewBag.Posts = SortNewHighLow(postIdList, sortBy);

            return View();
        }

        //Sort Sub-category posts by Newest, High Price or Low Price
        private dynamic SortNewHighLow(List<int> postIdList, string sortBy)
        {
            List<Post> postList = new List<Post>();

            for (int i = 0; i < postIdList.Count; i++)
            {
                var pId = postIdList[i];
                var post = db.Posts.Find(pId);
                postList.Add(post);
            }

            List<Post> postListSorted = new List<Post>();

            if (sortBy == "none")
            {
                postListSorted = postList;
            }
            else if (sortBy == "new")
            {
                postListSorted = postList.OrderByDescending(p => p.Created).ToList();
            }
            else if (sortBy == "high")
            {
                postListSorted = postList.OrderByDescending(p => p.Cost).ToList();
            }
            else if (sortBy == "low")
            {
                postListSorted = postList.OrderBy(p => p.Cost).ToList();
            }

            ViewBag.Posts = new List<PostViewModel>();

            foreach (var post in postListSorted)
            {
                PostViewModel pVM = new PostViewModel(post);
                ViewBag.Posts.Add(pVM);
            }
            return ViewBag.Posts;
        }


        //Categorize Posts
        private void CategorizePosts(List<Post> postList)
        {
            ViewBag.ForSale = new List<PostViewModel>();
            ViewBag.ForRent = new List<PostViewModel>();
            ViewBag.Services = new List<PostViewModel>();
            ViewBag.Uncategorized = new List<PostViewModel>();

            foreach (var post in postList)
            {
                PostViewModel pVM = new PostViewModel(post);
                if (pVM.Category != null)
                {
                    if (pVM.Category.ParentId == 4)
                    {
                        ViewBag.ForSale.Add(pVM);
                    }
                    else if (pVM.Category.ParentId == 5)
                    {
                        ViewBag.Services.Add(pVM);
                    }
                    else if (pVM.Category.ParentId == 10)
                    {
                        ViewBag.ForRent.Add(pVM);
                    }
                }
                else
                {
                    ViewBag.Uncategorized.Add(pVM);
                }
            };
        }
    }
}