using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class EFController : Controller
    {
        FabricsEntities db = new FabricsEntities();
 
        // GET: EF
        public ActionResult Index()
        {        
            var all = db.Product.AsQueryable();
            //var data = all.Where(p=> p.ClientId == 1);
            var data = all.Where(p => p.Active == true && p.ProductName.Contains("Black"));
            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Product.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        public ActionResult Edit(int id)
        {
            var item = db.Product.Find(id);
            return View(item);
        }
        [HttpPost]
        public ActionResult Edit(int id, Product product)
        {
            var item = db.Product.Find(id);
            item.ProductId = product.ProductId;
            item.Price = product.Price;

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var item = db.Product.Find(id);
            db.Product.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }

}