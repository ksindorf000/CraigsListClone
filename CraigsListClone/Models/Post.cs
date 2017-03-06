﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CraigsListClone.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public double Cost { get; set; }
        public DateTime Created { get; set; }
        
        public string OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public virtual ApplicationUser Owner { get; set; }

        public int CityId { get; set; }
        [ForeignKey("CityId")]
        public virtual City PostCity { get; set; }

    }
}