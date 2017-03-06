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
            var userId = User.Identity.GetUserId();

            if (userId != null)
            {
                var userCity = db.Users
                    .Where(u => u.Id == userId)
                    .Select(u => u.CityId)
                    .SingleOrDefault();

                return View(db.Posts
                    .Where(p => p.CityId == userCity)
                    .OrderByDescending(q => q.Created)
                    .ToList()
                    .Take(5));
            }

            return View(db.Posts
                   .OrderByDescending(q => q.Created)
                   .ToList()
                   .Take(5));
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