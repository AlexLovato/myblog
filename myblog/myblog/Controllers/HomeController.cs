using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myblog.Controllers
{
    public class HomeController : Controller
    {
        //set up a connection to the database
        Models.myblogEntities db = new Models.myblogEntities();
        //
        // GET: /Home/

        public ActionResult Index()
        {
            //pass all the posts to the view order by newest first.
            return View(db.posts.OrderByDescending(x => x.date));
        }

    }
}
