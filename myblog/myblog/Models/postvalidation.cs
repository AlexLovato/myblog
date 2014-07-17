using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//add the using for data annotations
using System.ComponentModel.DataAnnotations;

namespace myblog.Models
{
    //new to declare a partial class of what we want to validate
    [MetadataType(typeof(postvalidation))]
    public partial class post
    {

    }
    public class postvalidation
    {
        [Required, DataType(DataType.MultilineText)]
        public string body { get; set; }
        [Required, MaxLength(100)]
        public string title { get; set; }
        [MaxLength(200),DataType(DataType.ImageUrl)]
        public string imageURL { get; set; }
        
    }
}