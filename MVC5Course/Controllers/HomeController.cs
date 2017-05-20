using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [LocalOnly]
        [ShareViewBag]
        public ActionResult About()
        {
            //ViewBag.Message = "Your application description page."; 此段已在 [ShareViewBag]中執行,故可省略

            return View();
        }

        public ActionResult PartialAbout()
        {
            ViewBag.Message = "Your application description page.";

            if (Request.IsAjaxRequest())
            {
                return PartialView("About");
            }
            else
            {
                return View("About");
            }
        }

        public ActionResult SuccessRedirect()
        {
            return PartialView("SuccessRedirect" , "/");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Test()
        {
            return View();
        }

        public ActionResult GetFile()
        {
            return File(Server.MapPath("~/Content/Pokemon Go.png"), "image/png", "NewName.png");

        }

        public ActionResult GetJson()
        {
            //用來指定是否要啟用延遲加載,延時加載子對象不自動加載 false
            db.Configuration.LazyLoadingEnabled = false;
            return Json(db.Product.Take(5) , JsonRequestBehavior.AllowGet);
        }

   
        public ActionResult VT()
        {
            return View("VT");
        }

        public ActionResult RazotTest()
        {
            int[] data = new int[] { 1,2,3,4,5 };
            return PartialView(data);
        }

    }

}