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

        public virtual ICollection<Category> Categories { get; set; }

    }

    public class PostViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public double Cost { get; set; }
        public DateTime? Created { get; set; }
        public string OwnerId { get; set; }
        public int CityId { get; set; }
        public List<int> CatId { get; set; }
    }

}