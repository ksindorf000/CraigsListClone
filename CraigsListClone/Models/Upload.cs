using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CraigsListClone.Models
{
    public class UploadViewModel
    {
        [Required]
        public string Caption { get; set; }

        [Required]
        public HttpPostedFile File { get; set; }
    }

    public class Upload
    {
        public int Id { get; set; }
        public string Caption { get; set; }
        public string File { get; set; }
        public string TypeRef { get; set; }

        //If Type ProfilePic, ref == null. If Type PAttach, ref == Post.Id
        public int? RefId { get; set; }

        public string OwnerId { get; set; }

        public virtual ApplicationUser Owner { get; set; }

        public virtual string FilePath
        {
            get
            {
                return $"/Upload/{File}";
            }
        }

    }
}