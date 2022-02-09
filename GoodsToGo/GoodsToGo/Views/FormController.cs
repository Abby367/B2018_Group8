using GoodsToGo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GoodsToGo.Views
{
    
    public class FormController : Controller
    {
        Database1 db = new Database1();
        // GET: Form
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User us)
        {

            if (ModelState.IsValid)
            {
                db.Users.Add(us);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Forgot()
        {

            return View();
        }







        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User us)
        {
            var obj = db.Users.Where(x => x.User_Email.Equals(us.User_Email) && x.Password.Equals(us.Password)).FirstOrDefault();
            if (obj != null)
            {
                FormsAuthentication.SetAuthCookie(us.User_Email,false);
                return RedirectToAction("Inside","User");
            }
            else
            {
                {
                    ModelState.AddModelError("","Invalid User Email or Password");
                    return View();
                }
            }
        }

    }
}
