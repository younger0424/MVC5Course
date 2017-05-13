﻿using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
            var data = all.Where(p => p.Is刪除 == false && p.Active == true && p.ProductName.Contains("Black"));
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

            if (ModelState.IsValid)
            {
       
                item.ProductName = product.ProductName;
                item.Price = product.Price;
                item.Active = product.Active;
                item.Stock = product.Stock;

                db.SaveChanges();

                return RedirectToAction("Index");
            }else
            {
                return View(item);
            }
        }

        public ActionResult Delete(int id)
        {
            var item = db.Product.Find(id);
            //orderLine 的兩種寫法
            //foreach (var item1 in item.OrderLine.ToList())
            //{
            //    db.OrderLine.Remove(item1); 
            // }
            //db.OrderLine.RemoveRange(item.OrderLine);
            //db.Product.Remove(item);
            item.Is刪除 = true;
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
            
                return RedirectToAction("Index");
        }

        public ActionResult Details(int? id) //Medol Binding 模型繫結
        {

            var data = db.Database.SqlQuery<Product>("SELECT * FROM dbo.Product where ProductId = @p0", id).FirstOrDefault();

            //if (id == null)
            //{
            //    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Product product = db.Product.Find(id);
            //if (product == null)
            //{
            //    return HttpNotFound();
            //}
            return View(data);
        }
        
        public void RemoveAll()
        {
            //兩種刪除寫法
            //db.Product.RemoveRange(db.Product);
            //db.SaveChanges();

            db.Database.ExecuteSqlCommand("Delete from dbo.Product");
        }
    } 

}