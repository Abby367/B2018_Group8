using GoodsToGo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
    }
