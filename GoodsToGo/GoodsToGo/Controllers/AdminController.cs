using GoodsToGo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GoodsToGo.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        Database1 db = new Database1();
        // GET: Admin
        public ActionResult Admin()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Userxd()
        {
            var model = db.Users.ToList();
            return View(model);
            
        }
        [HttpPost]
        public ActionResult Userxd(User um)
        {

            ViewBag.BrgyList = new SelectList(GetBarangayList(), "BarangayID", "Barangay_Name");
            ViewBag.GenderList = new SelectList(GetGenderList(), "GenderID", "GenderName");
            if (ModelState.IsValid)
            {
                
                var data = db.Users.ToList();


                return View(data);
            }
            else
            {
                return View();

            }
        }
             [HttpGet]
        public ActionResult Appointment()
        {
            var model = db.Books.ToList();
            return View(model);

        }
        [HttpPost]
        public ActionResult Appointment(Book um)
        {
            if (ModelState.IsValid)
            {
                var data = db.Books.ToList();


                return View(data);
            }
            else
            {
                return View();
            }
        }
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            FormsAuthentication.SignOut();

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
            return RedirectToAction("Index", "Home");
        }
        public List<Barangay> GetBarangayList()
        {

            List<Barangay> brgy = db.Barangays.ToList();
            return brgy;
        }

        public List<Gender> GetGenderList()
        {
            List<Gender> Gen = db.Genders.ToList();
            return Gen;

        }


    }
}
