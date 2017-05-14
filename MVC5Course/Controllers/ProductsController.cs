using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using MVC5Course.Models.ViewModels;

namespace MVC5Course.Controllers
{
    public class ProductsController : BaseController
    {
        //private FabricsEntities db = new FabricsEntities();
        ProductRepository repo = RepositoryHelper.GetProductRepository();

        // GET: Products
        //public ActionResult Index()
        //{
        //    var db = repo.GetProduct所有資料();
        //    //return View(db.Product.OrderByDescending(p => p.ProductId).Take(10));
        //    return View(db);
        //}

        public ActionResult Index(bool? Active = true)
        {
            var db = repo.GetProduct所有資料(Active);
            //return View(db.Product.Where(p => p.Active.Value == Active).OrderByDescending(p => p.ProductId).Take(10));
            ViewData.Model = db;

            return View(db);
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id) //Medol Binding 模型繫結
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Product product = db.Product.Find(id);
            Product product = repo.Get單筆資料ByProductId(id.Value);
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
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                //db.Product.Add(product);
                repo.Add(product);
                //db.SaveChanges();
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Product product = db.Product.Find(id);
            Product product = repo.Get單筆資料ByProductId(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        public ActionResult Edit(int id , FormCollection form)
        {
            var product = repo.Get單筆資料ByProductId(id);
            if (TryUpdateModel(product,
                new string[] { "ProductId", "ProductName", "Price", "Active", "Stock" }))
            {
                repo.Update(product);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
             }

                //if (ModelState.IsValid)
                //{
                //    //db.Entry(product).State = EntityState.Modified;
                //    repo.Update(product);
                //    //db.SaveChanges();
                //    repo.UnitOfWork.Commit();
                //    return RedirectToAction("Index");
                //}
                return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Product product = db.Product.Find(id);
            Product product = repo.Get單筆資料ByProductId(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Product product = db.Product.Find(id);
            Product product = repo.Get單筆資料ByProductId(id);
            //db.Product.Remove(product);
            repo.Delete(product);
            //db.SaveChanges();
            repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        public ActionResult ListProducts(ProductListSearchVM searchCondition)
        {
            // var data = db.Product.Where(p => p.Active == true)
            var data = repo.GetProduct所有資料(true);

            if (!String.IsNullOrEmpty(searchCondition.q))
            {
                data = data.Where(p => p.ProductName.Contains(searchCondition.q));
            }

            //if (searchCondition.s != null) { data = data.Where(p => p.Price == searchCondition.s); }

            // data = data.Where(p => p.Stock > Stock_S && p.Stock < Stock_E);
            data = data.Where(p => p.Stock > searchCondition.Stock_S && p.Stock < searchCondition.Stock_E);
            var data2 = data.Select(p => new ProductListVM()
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    Stock = p.Stock
                }).Take(10);

            return View(data2);
        }

        public ActionResult CreateProduct()
        {
         
            return View();
        }

        [HttpPost]
        public ActionResult CreateProduct(ProductListVM data)
        {
            if (ModelState.IsValid)
            {
                TempData["CreateProduct_Result"] = "商品新增成功!!";
                return RedirectToAction("ListProducts");
            }

            return View();
        }


    }

}

