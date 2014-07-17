using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations; // Adding a using for Data annotations 
namespace myblog.Models
{
    public class login
    {[Required, Display(Name="User Name")]
        public string username {get; set;}
    [Required, DataType(DataType.Password)]
    public string password { get; set; }
    }
}