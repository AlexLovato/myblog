using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security; // add using for Memberships

namespace myblog.Controllers
{
    public class accountController : Controller
    {
        //Add a connection to the myBlog Database
        Models.myblogEntities db = new Models.myblogEntities();
        // GET: /account/

        public ActionResult Index()
        {
            //to create a user, type
            Membership.CreateUser("admin", "techIsFun1");

            //to check username & password
            if (Membership.ValidateUser("admin", "techIsFun1"))
            {
                FormsAuthentication.SetAuthCookie("admin", false);
            }
            return View();
        }
        //GET: /Account/Register
        public ActionResult Register()
        {
            //creating a blank registration model/object to pass to our view
            return View(new Models.register());
        }
        //POST: /ACCOUNT/REGIStER
        [HttpPost]
        public ActionResult Register(Models.register reg)
        {
            //FIRST check to see if the username is already in use
            if (Membership.GetUser(reg.username) == null)
            {
                //Its valid, add user to database
                Membership.CreateUser(reg.username, reg.password);
                //now let's add the user to myblog authors table
                //so we need to first make an object of that type..
                //make a new object
                Models.author author = new Models.author();
                author.username = reg.username;
                author.name = reg.name;
                author.imageURL = reg.imageurl;
                //now we add the author to the database
                db.authors.Add(author);
                db.SaveChanges();
                //Log the user in
                FormsAuthentication.SetAuthCookie(reg.username, true);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                //username is already in use

                ViewBag.Error = "Username is already in use good sir, please choose different one";
                //return the view with the reg object
                return View(reg);
            }
        }
        //GET: /Account/Logout
        [HttpGet]
        public ActionResult logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        //GET: /ACCOUNT/LOGIN
        [HttpGet]
        public ActionResult login()
        {
            return View(new Models.login());
        }

        [HttpPost]
        public ActionResult Login(Models.login login)
        {
            if (Membership.ValidateUser(login.username, login.password))
            {
                FormsAuthentication.SetAuthCookie(login.username, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.error = "Incorrect Username/Password. PLZ try again.";
                return View(login);
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
    
