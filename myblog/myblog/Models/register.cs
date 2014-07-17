using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace myblog.Models
{
    public class register
    {
        [Required]
        public string username { get; set; }
        [Required, DataType(DataType.Password)]
        public string password { get; set; }
        [Required, DataType(DataType.Password), Compare("password")]
        public string confirmpassword { get; set; }
        [Required, MaxLength(50)]
        public string name { get; set; }
        [DataType(DataType.ImageUrl)]
        public string imageurl { get; set; }
    }
}