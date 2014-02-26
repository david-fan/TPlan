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
            /*
            var plan = new TeachPlan.Plan();
            plan.Subject = new Subject() { Id = 2};
            plan.Name = "测试计划";
            plan.Author = "我自己";

            var p1 = new Point { Type = (int)PointType.Zhongdian, Content = "重点1" };
            p1.Method.Add(new Method { Content = "解决方法1" });
            p1.Method.Add(new Method { Content = "解决方法2" });
            var p2 = new Point { Type = (int)PointType.Nandian, Content = "难点1" };
            plan.Point.Add(p1);
            plan.Point.Add(p2);

            var t1 = new Target { Baseic = "基本目标1", Advance = "高级目标1", Type = 1 };
            var t2 = new Target { Baseic = "基本目标2", Advance = "高级目标2", Type = 1 };
            plan.Target.Add(t1);
            plan.Target.Add(t2);

            var a1 = new Active { Name = "活动1" };
            a1.Subject = db.SubjectSet.First();
            a1.Phase = db.PhaseSet.First();
            a1.Content = db.ContentSet.First();
            a1.Form = db.FormSet.First();
            a1.Steps.Add(new Step {  Name="步骤1",Content="步骤详细a"});
            a1.Steps.Add(new Step { Name = "步骤2", Content = "步骤详细b" });
            //var a2 = new Active { Name = "活动2" };
            plan.Active.Add(a1);
            //plan.Active.Add(a2);

            var th1 = new Think { Content = "反思1" };
            var j1 = new Judge { Content = "评价1" };
            plan.Think.Add(th1);
            plan.Judge.Add(j1);

            plan.ClassName = "课时名称test";
            plan.Organiser = "新学校123";

            plan.Grade = new Grade {Id=1 };
            plan.Textbook = new Textbook {Id=1 };
            */
            return View();
        }



        public ActionResult About()
        {
            return View();
        }



       
    }
}
