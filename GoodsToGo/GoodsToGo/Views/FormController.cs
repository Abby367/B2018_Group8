using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoodsToGo.Models;
namespace GoodsToGo.Controllers
{
    public class FormController : Controller
    {
        Database1 db = new Database1();
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(user us)
        {
            db.users.Add(us);
            db.SaveChanges();
            return RedirectToAction("Login");
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(user us)
        {
            var obj = db.users.Where(x => x.User_Name.Equals(us.User_Name) && x.Password.Equals(us.Password)).FirstOrDefault();
            if (obj != null)
            {
                return RedirectToAction("Employ", "Form");

            }
            else if (us.User_Name == "admin" && us.Password == "admin")
            {
                return RedirectToAction("Admin");
            }
            return View();
        }
       
        public ActionResult Forgot()
        {
            return View();
        }
        public ActionResult Employ()
        {
            return View();
        }

    }
}