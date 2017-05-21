using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class FormController : BaseController
    {
        //private FabricsEntities db = new FabricsEntities();
        // GET: Form
        public ActionResult Index()
        {
            return View();
        }
        //Get
        public ActionResult Edit(int id)
        {
            ViewData.Model = db.Product.Find(id);

            return View();
        }

        [HttpPost]
        public ActionResult Edit(int id, System.Web.Mvc.FormCollection form)
        {
            var data = db.Product.Find(id);
            if (ModelState.IsValid) {

                if (TryUpdateModel(data,
                    includeProperties: new string[] { "ProductName" }))
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                } 

            }
            
            return View();
        }
    }
}