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
        //get: /post/delete/{id}
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Models.post postToDelete = db.posts.Find(id);
            return View(postToDelete);
        }
        //Post: post/delete/{id}
        //using actionname("delete") so the URL stays the same, but the function can have a different name
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            //get the post from the db
            Models.post postToDelete = db.posts.Find(id);
            db.posts.Remove(postToDelete);
            db.SaveChanges();
            return RedirectToAction("Index", "Post");

        }
        //get post/edit/id
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Models.post postToEdit = db.posts.Find(id);
            //pass our post to edit to View
            return View(postToEdit);
        }
        [HttpPost]
        public ActionResult Edit(Models.post postToEdit)
        {
            //set the post to be upddated
            db.Entry(postToEdit).State = System.Data.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Post");
        }
       
            
        
    }
}
