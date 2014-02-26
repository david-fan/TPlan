using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

using System.Text;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace TeachPlan.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LogOn()
        {
            var ui = new UserInfo();
            ui.Name = "test";
            ui.PWD = "";
            return View(ui);
        }
        [HttpPost]
        public ActionResult LogOn(UserInfo ui, string returnUrl)
        {
            var service = new UserInfoService();
            var userInfo = service.Login(ui.Name, ui.PWD);
            if (userInfo != null)
            {
                Session["user"] = userInfo.MyId;
                System.Web.Security.FormsAuthentication.SetAuthCookie(userInfo.Name, false);
                //Response.Redirect("~/Plan/Index");
            }
            return View();
        }

    }
}
