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
        //get: /home/like/id
        public ActionResult Like(int id)
        {
            //go to the DB and retrieve the post taht matches the ID
            Models.post post = db.posts.Find(id);
            post.likes = post.likes + 1;
            db.SaveChanges();
            //return the number of likes
            //if (post.likes == 1)
            //{
            //    return Content(post.likes + " like");
            //}
            //else 
            return Content(post.likes + " likes");
        }
        //AJAX Post: /home/addComment
        public ActionResult addComment(Models.comment c){
            //set the other properties of the comment
            c.date = DateTime.Now;
            //add it to the db
            db.comments.Add(c);
            //save the changes
            db.SaveChanges();
            //return something
            return PartialView("comment", c);
        }
    }
}
