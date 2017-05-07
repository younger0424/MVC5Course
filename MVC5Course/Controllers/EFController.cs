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
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
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
            item.ProductName = product.ProductName;
            item.Price = product.Price;
            item.Active = product.Active;
            item.Stock = product.Stock;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var item = db.Product.Find(id);
            //orderLine 的兩種寫法
            //foreach (var item1 in item.OrderLine.ToList())
            //{
            //    db.OrderLine.Remove(item1); 
            // }
            db.OrderLine.RemoveRange(item.OrderLine);
            db.Product.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id) //Medol Binding 模型繫結
        {

            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
    }

}