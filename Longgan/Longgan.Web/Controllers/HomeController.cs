using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Longgan.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Message()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Advantage()
        {
            ViewBag.Message = "Your Advantage page.";

            return View();
        }

        public ActionResult News()
        {
            return View();
        }

        public ActionResult Case()
        {
            return View();
        }
        public ActionResult CaseDetail()
        {
            return View();
        }
    }
}