using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeachPlan.Controllers
{
	public class ActiveController : Controller
	{
		public ActionResult Index ()
		{
			return View ();
		}

		public PartialViewResult Active(Active modle)
		{
			return PartialView("_Active",modle);
		}

		[HttpGet]
		public PartialViewResult AddActive (int planId)
		{
			var active = new Active ();
			if (planId > 0) {
				if (active.Plan_Ids == null)
					active.Plan_Ids = new List<int>();
				active.Plan_Ids.Add (planId);
			}

			return PartialView ("_AddActive", active);
		}

		[HttpPost]
		public JsonResult AddActive (Active model)
		{
			var service = new ActiveService ();
			model.UserInfo_Id = int.Parse (Session ["user"].ToString ());
			model.Steps = Request.Form ["addsteps"].Split ('$').ToList ();
			service.Create (model);
			return Json (true);
		}

		[HttpPost]
		public JsonResult AddComment (Comment comment)
		{
			var service = new CommentService ();
			service.Create (comment);
			return Json (true);
		}

		[HttpGet]
		public PartialViewResult AddComment (int activeId)
		{
			var comment = new Comment ();
			comment.To_Type = (int)ToType.Active;
			comment.To_Id = activeId;
			return PartialView ("_AddComment", comment);
		}

		public PartialViewResult Subjects ()
		{
			var service = new SubjectService ();
			return PartialView ("_Subjects", service.GetAll ());
		}

		public PartialViewResult Phases ()
		{
			var service = new PhaseService ();
			return PartialView ("_Phases", service.GetAll ());
		}

		public PartialViewResult Forms ()
		{
			var service = new FormService ();
			return PartialView ("_Forms", service.GetAll ());
		}

		public PartialViewResult Contents ()
		{
			var service = new ContentService ();
			return PartialView ("_Contents", service.GetAll ());
		}

		[HttpPost]
		public PartialViewResult SearchActives ()
		{
			var forms = Request.Form;
			var phase = forms.Get ("phase");
			var form = forms.Get ("form");
			var content = forms.Get ("content");
			var subject = forms.Get ("subject");

			return SearchActives ((int)Session ["user"],
				(form != null) ? int.Parse (form) : 0,
				(phase != null) ? int.Parse (phase) : 0,
				(content != null) ? int.Parse (content) : 0,
				(subject != null) ? int.Parse (subject) : 0);
		}

		public PartialViewResult SearchActives (int userId, int formId, int subjectId, int phaseId, int contentId)
		{
			var service = new ActiveService ();
			var enumer = service.GetByUserId (userId, formId, subjectId, phaseId, contentId);
			var list = new List<TPlan.ActiveDetailModel> ();
			var e = enumer.GetEnumerator ();
			while (e.MoveNext ()) {
				list.Add (new TPlan.ActiveDetailModel (e.Current));
			}
			return PartialView ("_Actives", list);
		}

		public static IEnumerable<SelectListItem> getAllTargetType ()
		{
			var grades = new List<SelectListItem> ();
			//var service = new ContentService();
			var g = SystemConst.TargetTypeName.GetEnumerator ();
			while (g.MoveNext ()) {
				var sli = new SelectListItem ();
				sli.Text = g.Current.Value;
				sli.Value = ((int)g.Current.Key).ToString ();
				grades.Add (sli);
			}
			return grades;
		}

		public static IEnumerable<SelectListItem> getAllPoint ()
		{
			var grades = new List<SelectListItem> ();
			//var service = new ContentService();
			var g = SystemConst.PointTypeName.GetEnumerator ();
			while (g.MoveNext ()) {
				var sli = new SelectListItem ();
				sli.Text = g.Current.Value;
				sli.Value = ((int)g.Current.Key).ToString ();
				grades.Add (sli);
			}
			return grades;
		}
	}
}
