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

namespace GoodsToGo.Views
{
    [Authorize]
    public class UserController : Controller
    {
        Database1 db = new Database1();
        // GET: User
        public ActionResult Inside()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateProfile()
        {

            ViewBag.BrgyList = new SelectList(GetBarangayList(), "BarangayID", "Barangay_Name");

            ViewBag.GenderList = new SelectList(GetGenderList(), "GenderID", "GenderName");
            return View();
        }

        [HttpPost]

        public ActionResult CreateProfile(Profile us)
        {
            ViewBag.BrgyList = new SelectList(GetBarangayList(), "BarangayID", "Barangay_Name");

            ViewBag.GenderList = new SelectList(GetGenderList(), "GenderID", "GenderName");


            if (ModelState.IsValid)
            {
                db.Profiles.Add(us);
                db.SaveChanges();
             
            }
            return View();
        }

        [Route("User/Edit/{id}")]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.BrgyList = new SelectList(GetBarangayList(), "BarangayID", "Barangay_Name");

            ViewBag.GenderList = new SelectList(GetGenderList(), "GenderID", "GenderName");


            return View(db.Profiles.OrderByDescending(x => x.Id == id).FirstOrDefault());




        }



        [Route("User/Edit/{id}")]
        [HttpPost]
        public ActionResult Edit(int id, Profile pr)
        {
            ViewBag.BrgyList = new SelectList(GetBarangayList(), "BarangayID", "Barangay_Name");
            ViewBag.GenderList = new SelectList(GetGenderList(), "GenderID", "GenderName");


            db.Entry(pr).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Inside");
        }

        [HttpGet]
        public ActionResult History()
        {
            var model = db.Books.ToList();
            return View(model);

        }
        [HttpPost]
        public ActionResult History(Book um)
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
        [HttpGet]
        public ActionResult Book()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Book(Book ut)
        {
            if (ModelState.IsValid)
            {
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = new NetworkCredential("taguigpublicgoodsdistribution@gmail.com", "Unikon34");
                MailMessage msg = new MailMessage("taguigpublicgoodsdistribution@gmail.com", ut.Email);
                msg.Subject = "Mail From Taguig Goods Distribution";
                msg.Body = "Magandang Araw!" + "\r\n\r\n" + "Kayo po ay naischedule na kumuha ng inyong foodpack sa pinakamalapit na Barangay " + "\r\n\r\n" + "\r\n\r\n" + "Maari lamang po na alalahin ang mga sumusunod na detalye" + "\r\n\r\n" + "Name: " + ut.Name + "\r\n\r\n" + "Date of Schedule: " + ut.Date + "\r\n\r\n" + "Time of Schedule: " + ut.Time + "\r\n\r\n" + "Maaring Pumunta lamang sa takdang oras na inyong inischedule";
                db.Books.Add(ut);
                db.SaveChanges();
                smtp.Send(msg);
                ViewBag.Message = "You Successfully Book your Schedule";
                return RedirectToAction("Index");

            }
            else
            {
                return View();
            }

        }
        [HttpGet]
        public ActionResult Details(int id)
        {
           
            ViewBag.BrgyList = new SelectList(GetBarangayList(), "BarangayID", "Barangay_Name");
            ViewBag.GenderList = new SelectList(GetGenderList(), "GenderID", "GenderName");
           
            return View(db.Profiles.OrderByDescending(x => x.Id == id).FirstOrDefault());

        }
        [HttpPost]
        public ActionResult Details(Profile um,int id)
        {
            db.Entry(um).State = EntityState.Unchanged;
            return RedirectToAction("Inside");
        }    
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.RemoveAll();
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