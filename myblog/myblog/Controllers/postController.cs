using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myblog.Controllers
{
    //Authorize data annotation requires user to be logged in to access anything in this controller
    [Authorize()]
    public class postController : Controller
    {
        //make a connection to the database
        Models.myblogEntities db = new Models.myblogEntities();
        //
        // GET: /post/

        public ActionResult Index()
        {
            return View(db.posts.Where(x=>x.username.ToLower() == User.Identity.Name.ToLower()));
        }
        //index will return all the user's posts
        
        //GET: /POST/Create
        [HttpGet]
        public ActionResult Create()
        {
            //pass a blank post object to the view

            return View(new Models.post());
        }
        //POST: /POST/create
        [HttpPost]
        public ActionResult Create(Models.post post)
        {
            post.username = User.Identity.Name;
            post.date = DateTime.Now;
            post.likes = 0;
            //make sure that imgurl has a value
            if (post.imageURL == null)
            {
                post.imageURL = "";
            }
            //ad the post to the database
            db.posts.Add(post);
            db.SaveChanges();
            return RedirectToAction("Index", "Post");
        }
    }
}
