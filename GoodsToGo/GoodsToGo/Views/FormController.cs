using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GoodsToGo.Views
{
    public class FormController : Controller
    {
        // GET: Form
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Forgot()
        {
            return View();
        }
    }
}