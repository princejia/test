using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Longgan.Models.Home;
using Longgan.Web.Models;
using Longgan.Logics.Home;
using System.Configuration;
using System.IO;

namespace Longgan.Web.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        ProductsLogic logic = new ProductsLogic();
        // GET: Products
        public ActionResult Index()
        {
            return View(logic.GetProducts());
        }

        // GET: Products/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = logic.GetProduct(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,Title,Content,PicName,IntroName,Created")] Product product, FormCollection fc)
        {
            string filePath = ConfigurationManager.AppSettings["fileRoot"];
            if (!Directory.Exists(filePath + "\\Product"))
            {
                Directory.CreateDirectory(filePath + "\\Product");
            }
            if (!Directory.Exists(filePath + "\\Intro"))
            {
                Directory.CreateDirectory(filePath + "\\Intro");
            }

            if (ModelState.IsValid)
            {
                HttpPostedFileBase filePic = Request.Files["picProduct"];
                if (!string.IsNullOrEmpty(filePic.FileName))
                {
                    string fileName = "Product" + DateTime.Now.ToString("yyyyMMddhhmmss");
                    string path = string.Format("{0}\\Product\\{1}{2}", filePath, fileName, Path.GetExtension(filePic.FileName));
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                    filePic.SaveAs(path);
                    product.PicName = fileName + Path.GetExtension(filePic.FileName);
                }

                HttpPostedFileBase fileIntro = Request.Files["picIntro"];
                if (!string.IsNullOrEmpty(fileIntro.FileName))
                {
                    string fileName = "Intro" + DateTime.Now.ToString("yyyyMMddhhmmss");
                    string path = string.Format("{0}\\Intro\\{1}{2}", filePath, fileName, Path.GetExtension(fileIntro.FileName));
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                    filePic.SaveAs(path);
                    product.IntroName = fileName + Path.GetExtension(fileIntro.FileName);
                }

                logic.AddProduct(product);
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = logic.GetProduct(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Content,PicName,IntroName,Created")] Product product)
        {
            if (ModelState.IsValid)
            {
                logic.UpdateProduct(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = logic.GetProduct(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Product product = logic.GetProduct(id);
            logic.RemoveProduct(product);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
