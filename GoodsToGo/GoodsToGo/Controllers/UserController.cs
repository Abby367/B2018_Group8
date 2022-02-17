using GoodsToGo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Security.Claims;
namespace GoodsToGo.Views
{
    [Authorize]
    public class UserController : Controller
    {
        Database1 db = new Database1();
       

       
        public ActionResult Inside()
        {
            var obj = db.Users.FirstOrDefault(x => x.Email == System.Web.HttpContext.Current.User.Identity.Name).Id;
            ViewData["um"] = obj;
            var obm = db.Users.FirstOrDefault(x => x.Email == System.Web.HttpContext.Current.User.Identity.Name).First_Name;
            ViewBag.name = obm;
            var bk = db.Books.OrderByDescending(p => p.Id).First().Id;
            ViewData["vm"] = bk;

            return View();
        }
        
        [HttpGet]
        
        public ActionResult Edit(int id)
        {
            var obj = db.Users.FirstOrDefault(x => x.Email == System.Web.HttpContext.Current.User.Identity.Name).Id;
            ViewData["um"] = obj;
            ViewBag.BrgyList = new SelectList(GetBarangayList(), "BarangayID", "Barangay_Name");
            ViewBag.GenderList = new SelectList(GetGenderList(), "GenderID", "GenderName");
           
            return View(db.Users.Where(x => x.Id == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(User user)
        {
            
           
            ViewBag.BrgyList = new SelectList(GetBarangayList(), "BarangayID", "Barangay_Name");
            ViewBag.GenderList = new SelectList(GetGenderList(), "GenderID", "GenderName");
            
           
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Inside");
           
        }




        [HttpGet]
        public ActionResult History(int id)
        {
            var obj = db.Users.FirstOrDefault(x => x.Email == System.Web.HttpContext.Current.User.Identity.Name).Id;
            ViewData["um"] = obj;
            return View(db.Books.Where(x => x.Id == id).FirstOrDefault());
        

    }
        [HttpPost]
        public ActionResult History(Book um)
        {
            var obj = db.Users.FirstOrDefault(x => x.Email == System.Web.HttpContext.Current.User.Identity.Name).Id;
            ViewData["um"] = obj;
           
           
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
        [HttpGet]
        public ActionResult Book()
        {
            var obj = db.Users.FirstOrDefault(x => x.Email == System.Web.HttpContext.Current.User.Identity.Name).Id;
            ViewData["um"] = obj;
            return View();
        }

        [HttpPost]
        public ActionResult Book(Book ut)
        {
            var obj = db.Users.FirstOrDefault(x => x.Email == System.Web.HttpContext.Current.User.Identity.Name).Id;
            ViewData["um"] = obj;


            var an = db.Users.FirstOrDefault(x => x.Email == System.Web.HttpContext.Current.User.Identity.Name).Id;
            var om = db.Users.FirstOrDefault(x => x.Email == System.Web.HttpContext.Current.User.Identity.Name).Email;
            var fn = db.Users.FirstOrDefault(x => x.Email == System.Web.HttpContext.Current.User.Identity.Name).First_Name;
            var ln = db.Users.FirstOrDefault(x => x.Email == System.Web.HttpContext.Current.User.Identity.Name).Last_Name;




            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = new NetworkCredential("taguigpublicgoodsdistribution@gmail.com", "Unikon34");
            MailMessage msg = new MailMessage("taguigpublicgoodsdistribution@gmail.com", om);
            msg.Subject = "Mail From Taguig Goods Distribution";
            msg.Body = "Magandang Araw!" + "\r\n\r\n" + "Kayo po ay nagischedule na kumuha ng inyong foodpack. Pumunta lamang sa pinakamalapit na Barangay " + "\r\n\r\n" + "\r\n\r\n" + "Maari lamang po na alalahin ang mga sumusunod na detalye" + "\r\n\r\n" + "Appointment Number: " + an + "Name: " + fn + " " + ln + "\r\n\r\n" + "Date of Schedule: " + ut.Date + "\r\n\r\n" + "Time of Schedule: " + ut.Time + "\r\n\r\n" + "Maaring Pumunta lamang sa takdang oras na inyong inischedule";
            ut.Id = an;
            ut.Email = om;
            ut.Name = fn + " " + ln;
            db.Books.Add(ut);
            db.SaveChanges();
            smtp.Send(msg);
            ViewBag.Message = "You Successfully Book your Schedule";
            return RedirectToAction("Inside");
        }
      
        public ActionResult Details(int id)
        {
            var obj = db.Users.FirstOrDefault(x => x.Email == System.Web.HttpContext.Current.User.Identity.Name).Id;
            ViewData["um"] = obj;
            ViewBag.BrgyList = new SelectList(GetBarangayList(), "BarangayID", "Barangay_Name");

            ViewBag.GenderList = new SelectList(GetGenderList(), "GenderID", "GenderName");

            return View(db.Users.Where(x => x.Id == id).FirstOrDefault());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(User user)
        {
            var obj = db.Users.FirstOrDefault(x => x.Email == System.Web.HttpContext.Current.User.Identity.Name).Id;
            ViewData["um"] = obj;
            var obm = db.Users.FirstOrDefault(x => x.Email == System.Web.HttpContext.Current.User.Identity.Name).First_Name;
            ViewBag.name = obm;
            var om = db.Books.FirstOrDefault(x => x.Email == System.Web.HttpContext.Current.User.Identity.Name).Id;
            ViewData["un"] = om;
            ViewBag.BrgyList = new SelectList(GetBarangayList(), "BarangayID", "Barangay_Name");

            ViewBag.GenderList = new SelectList(GetGenderList(), "GenderID", "GenderName");

            
                return RedirectToAction("Inside");
            
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

        public ActionResult Privacy()
        {
            var obj = db.Users.FirstOrDefault(x => x.Email == System.Web.HttpContext.Current.User.Identity.Name).Id;
            ViewData["um"] = obj;
            return View();
        }
        public ActionResult Term()
        {
            var obj = db.Users.FirstOrDefault(x => x.Email == System.Web.HttpContext.Current.User.Identity.Name).Id;
            ViewData["um"] = obj;
            return View();
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