using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CraigsListClone.Models
{
    public class UserPrefData
    {
        [Key]
        public string UserId { get; set; }
        public int CityId { get; set; }
    }
}