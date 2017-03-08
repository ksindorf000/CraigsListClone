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
            ViewBag.ForSale = new List<PostViewModel>();
            ViewBag.ForRent = new List<PostViewModel>();
            ViewBag.Services = new List<PostViewModel>();
            ViewBag.Uncategorized = new List<PostViewModel>();

            List<Post> postList = db.Posts.OrderByDescending(p => p.Created).ToList();
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

            return View();
        }

        public ActionResult CitySort(int id)
        {
            ViewBag.Cities = db.Cities.ToList();

            ViewBag.PostVMList = new List<PostViewModel>();
            List<Post> postList = db.Posts
                .Where(p => p.CityId == id)
                .OrderByDescending(p => p.Created)
                .ToList();

            foreach (var post in postList)
            {
                PostViewModel pVM = new PostViewModel(post);
                ViewBag.ForSale.Add(pVM);
            }
            return View();
        }
    }
}