using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeachPlan.Controllers
{
    public class HomeController : Controller
    {
        

        public ActionResult Index()
        {
            if (!this.Request.IsAuthenticated)
            {
                return RedirectToAction("LogOn", "Account");
            }
            return View();
        }



        public ActionResult About()
        {
            return View();
        }



       
    }
}
