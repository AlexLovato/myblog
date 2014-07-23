using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using myblog.Models;

namespace myblog.Controllers
{
    public class CommentsController : Controller
    {
        private myblogEntities db = new myblogEntities();

        //
        // GET: /Comments/

        public ActionResult Index()
        {
            var comments = db.comments.Include(c => c.post);
            return View(comments.ToList());
        }

        //
        // GET: /Comments/Details/5

        public ActionResult Details(int id = 0)
        {
            comment comment = db.comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        //
        // GET: /Comments/Create

        public ActionResult Create()
        {
            ViewBag.postID = new SelectList(db.posts, "postID", "username");
            return View();
        }

        //
        // POST: /Comments/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(comment comment)
        {
            if (ModelState.IsValid)
            {
                db.comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.postID = new SelectList(db.posts, "postID", "username", comment.postID);
            return View(comment);
        }

        //
        // GET: /Comments/Edit/5

        public ActionResult Edit(int id = 0)
        {
            comment comment = db.comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.postID = new SelectList(db.posts, "postID", "username", comment.postID);
            return View(comment);
        }

        //
        // POST: /Comments/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.postID = new SelectList(db.posts, "postID", "username", comment.postID);
            return View(comment);
        }

        //
        // GET: /Comments/Delete/5

        public ActionResult Delete(int id = 0)
        {
            comment comment = db.comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        //
        // POST: /Comments/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            comment comment = db.comments.Find(id);
            db.comments.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}