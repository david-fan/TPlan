using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeachPlan.Controllers
{
    public class ActiveController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
		public PartialViewResult AddActive(int planId)
        {
			var active = new Active ();
			active.Plan_Id = planId;
			return PartialView("_AddActive",active);
        }

        [HttpPost]
        public JsonResult AddActive(Active model)
        {
            var service = new ActiveService();
            model.UserInfo_Id = int.Parse(Session["user"].ToString());
			model.Steps = Request.Form["addsteps"].Split('$').ToList();
			service.Create (model);
            return Json(true);
        }

		[HttpPost]
		public JsonResult AddComment(Comment comment)
		{
			var service = new CommentService();
			service.Create(comment);
			return Json(true);
		}

		[HttpGet]
		public PartialViewResult AddComment(int activeId)
		{
			var comment = new Comment();
			comment.To_Type = (int)ToType.Active;
			comment.To_Id = activeId;
			return PartialView ("_AddComment", comment);
		}

        public PartialViewResult Subjects()
        {
            var service = new SubjectService();
            return PartialView("_Subjects", service.GetAll());
        }

        public PartialViewResult Phases()
        {
            var service = new PhaseService();
            return PartialView("_Phases", service.GetAll());
        }

        public PartialViewResult Forms()
        {
            var service = new FormService();
            return PartialView("_Forms", service.GetAll());
        }

        public PartialViewResult Contents()
        {
            var service = new ContentService();
            return PartialView("_Contents", service.GetAll());
        }

		public PartialViewResult SerachActives(int userId,int formId,int subjectId,int phaseId,int contentId)
		{
			var service = new ActiveService();
			var enumer = service.GetByUserId(userId,formId,subjectId,phaseId,contentId);
			return PartialView("_Actives", enumer);
		}


        public static IEnumerable<SelectListItem> getAllTargetType()
        {
            var grades = new List<SelectListItem>();
			//var service = new ContentService();
            var g = SystemConst.TargetTypeName.GetEnumerator();
            while (g.MoveNext())
            {
                var sli = new SelectListItem();
                sli.Text = g.Current.Value;
                sli.Value = ((int)g.Current.Key).ToString();
                grades.Add(sli);
            }
            return grades;
        }

        public static IEnumerable<SelectListItem> getAllPoint()
        {
            var grades = new List<SelectListItem>();
			//var service = new ContentService();
            var g = SystemConst.PointTypeName.GetEnumerator();
            while (g.MoveNext())
            {
                var sli = new SelectListItem();
                sli.Text = g.Current.Value;
                sli.Value = ((int)g.Current.Key).ToString();
                grades.Add(sli);
            }
            return grades;
        }
    }
}
