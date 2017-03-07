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
            List<PostViewModel> pvmList = new List<PostViewModel>();
            List<Post> postList = db.Posts.OrderByDescending(p => p.Created).ToList();
            foreach (var post in postList)
            {
                PostViewModel pVM = new PostViewModel(post);
                pvmList.Add(pVM);
            };
            return View(pvmList);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}