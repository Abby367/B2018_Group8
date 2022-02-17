using GoodsToGo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;
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
            ViewBag.BrgyList = new SelectList(GetBarangayList(), "BarangayID", "Barangay_Name");

            ViewBag.GenderList = new SelectList(GetGenderList(), "GenderID", "GenderName");
            return View();
        }
        [HttpPost]
       
        public ActionResult Register(User us)
        {
            ViewBag.BrgyList = new SelectList(GetBarangayList(), "BarangayID", "Barangay_Name");
            ViewBag.GenderList = new SelectList(GetGenderList(), "GenderID", "GenderName");
            
                db.Users.Add(us);
                db.SaveChanges();
                return RedirectToAction("Login");
           
        }

        [NonAction]
public void SendVerificationLinkEmail(string emailID, string activationCode, string emailFor = "VerifyAccount")
        {
            var verifyUrl = "/Form/" + emailFor + "/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("taguigpublicgoodsdistribution@gmail.com", "Goods To Go");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "Unikon34"; // Replace with actual password

            string subject = "";
            string body = "";
          
           if (emailFor == "ResetPassword")
            {
                subject = "Reset Password";
                body = "Hi,<br/><br/>You Recently requested to reset your password for your account. Click the link below" +
                    "<br/><br/><a href=" + link + ">Reset Password link</a> <br/><br/>" +
                    "<br/><br/>If you did not request password reset please ignore this email or reply to let us know ";
            }


            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }
        [HttpGet]
        public ActionResult Forgot()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Forgot(User em)
        {
            string message = "";
           

           
                var account = db.Users.Where(a => a.Email == em.Email).FirstOrDefault();
                if (account != null)
                {
                    //Send email for reset password
                    string resetCode = Guid.NewGuid().ToString();
                    SendVerificationLinkEmail(account.Email, resetCode, "ResetPassword");
                    account.ResetPassCode = resetCode;
                    //This line I have added here to avoid confirm password not match issue , as we had added a confirm password property 
                    //in our model class in part 1
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.SaveChanges();
                    message = "Reset password link has been sent to your email id.";
                }
                else
                {
                    message = "Account not found";
                
            }
            ViewBag.Message = message;
            return View();
        }

        [HttpGet]
            public ActionResult ResetPassword(string id)
        {

            if (string.IsNullOrWhiteSpace(id))
            {
                return HttpNotFound();
            }

           
                var user = db.Users.Where(a => a.ResetPassCode == id).FirstOrDefault();
                if (user != null)
                {
                    ResetPassword model = new ResetPassword();
                    model.ResetCode = id;
                    return View(model);
                }
                else
                {
                    return HttpNotFound();
                
            }
        }
        [HttpPost]
        public ActionResult ResetPassword(ResetPassword model)
        {

            var message = "";
            if (ModelState.IsValid)
            {
              
                    var user = db.Users.Where(a => a.ResetPassCode == model.ResetCode).FirstOrDefault();
                    if (user != null)
                    {
                        user.Password = Crypto.Hash(model.NewPassword);
                        user.ResetPassCode = "";
                        db.Configuration.ValidateOnSaveEnabled = false;
                        db.SaveChanges();
                        message = "New password updated successfully";
                    
                }
            }
            else
            {
                message = "Something invalid";
            }
            ViewBag.Message = message;
            return View(model);
        }


        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.BrgyList = new SelectList(GetBarangayList(), "BarangayID", "Barangay_Name");
            ViewBag.GenderList = new SelectList(GetGenderList(), "GenderID", "GenderName");
            return View();
        }
        [HttpPost]
        
        public ActionResult Login(User us)
        {
            ViewBag.BrgyList = new SelectList(GetBarangayList(), "BarangayID", "Barangay_Name");
            ViewBag.GenderList = new SelectList(GetGenderList(), "GenderID", "GenderName");
            var obj = db.Users.Where(x => x.Email.Equals(us.Email) && x.Password.Equals(us.Password)).FirstOrDefault();
            if (obj != null)
            {
                FormsAuthentication.SetAuthCookie(us.Email, false);
                return RedirectToAction("Inside", "User");
            }
            else if (us.Email == "admin@gmail.com" && us.Password == "admin")
            {
                FormsAuthentication.SetAuthCookie(us.Email, false);
                return RedirectToAction("Admin", "Admin");
            }
            else
            {

              ModelState.AddModelError("Error","Invalid User Email or Password");
                return View();
            
                
            }
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
