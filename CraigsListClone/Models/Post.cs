using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CraigsListClone.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public double Cost { get; set; }
        public DateTime? Created { get; set; }
        
        public string OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public virtual ApplicationUser Owner { get; set; }

        public int CityId { get; set; }
        [ForeignKey("CityId")]
        public virtual City PostCity { get; set; }
    }

    public class PostViewModel
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public int Id { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public double Cost { get; set; }
        public DateTime? Created { get; set; }
        public string OwnerId { get; set; }
        public virtual ApplicationUser Owner { get; set; }
        public int CityId { get; set; }
        public string CatName { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }

        public PostViewModel()
        {

        }

        public PostViewModel(Post post)
        {
            Id = post.Id;
            Title = post.Title;
            Desc = post.Desc;
            Cost = post.Cost;
            Created = post.Created;
            OwnerId = post.OwnerId;
            Owner = post.Owner;
            CityId = post.CityId;
            CatName = db.PostCategories
                .Where(pc => pc.PostId == post.Id)
                .Select(pc => pc.Category.Name).FirstOrDefault();
        }

    }

}