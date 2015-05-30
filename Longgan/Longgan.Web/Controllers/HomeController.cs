using Longgan.Logics.Home;
using Longgan.Models.Home;
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

        public ActionResult Contact(string success)
        {
            @ViewBag.Success = success;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(Message msg)
        {
            MessageLogic logic = new MessageLogic();
            if (ModelState.IsValid)
            {
                logic.AddMessage(msg);
                return RedirectToAction("Contact", new { success = "留言成功" });
            }

            return View(msg);
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
            NewsLogic nLogic = new NewsLogic();
            List<New> news = nLogic.GetNews();
            return View(news);
        }

        public ActionResult Case()
        {
            return View();
        }
        public ActionResult CaseDetail()
        {
            return View();
        }

        public ActionResult ProductsList()
        {
            return View();
        }

        public ActionResult ProductsDetail()
        {
            return View();
        }

        public ActionResult AfterSale()
        {
            return View();
        }
    }
}