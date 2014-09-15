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

		public PartialViewResult Active (Active modle)
		{
			return PartialView ("_Active", modle);
		}

		[HttpGet]
		public PartialViewResult AddActive (int planId)
		{
			var active = new Active ();
			active.Plan_Id = planId;
			return PartialView ("_AddActive", active);
		}

		[HttpPost]
		public JsonResult AddActive (Active model)
		{
			var service = new ActiveService ();
			model.UserInfo_Id = int.Parse (Session ["user"].ToString ());
			model.Steps = new List<ActiveStep> ();
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

			return SearchActivesBy ((int)Session ["user"],
			                        (form != null) ? int.Parse (form) : 0,
			                        (phase != null) ? int.Parse (phase) : 0,
			                        (content != null) ? int.Parse (content) : 0,
			                        (subject != null) ? int.Parse (subject) : 0);
		}

		/**
		public PartialViewResult SearchActives()
		{
			return SearchActives ((int)Session ["user"], 0, 0, 0, 0);
		}
**/
		public PartialViewResult SearchActivesBy (int userId, int formId, int subjectId, int phaseId, int contentId)
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

		public ViewResult EditActive (int activeId)
		{
			var service = new ActiveService ();
			var active = service.GetById (activeId);
			return View ("_EditActive", active);
		}

		public PartialViewResult ShowPreSetSteps (int acitveId)
		{
			ViewData ["activeId"] = acitveId;
			var service = new PreSetStepService ();
			return PartialView ("_PreSetSteps", service.GetAll ());
		}

		public PartialViewResult ShowActiveSteps(Active active)
		{
			return PartialView ("_ActiveSteps", active);
		}

		public PartialViewResult ReOrderActiveStep(int orderId,string type,int activeId)
		{
			var service = new ActiveService ();
			var tempId = 0;
			if (type == "DOWN") {
				tempId = orderId + 1;
			} else if (type == "UP") {
				tempId = orderId - 1;
			}
			var active = service.GetById (activeId);
			foreach (var s in active.Steps) {
				if (s.OrderId == tempId)
					s.OrderId = orderId;
				else if (s.OrderId == orderId)
					s.OrderId = tempId;
			}
			active.Steps.Sort ();
			service.UpdateSteps (activeId, active.Steps);
			return PartialView ("_ActiveSteps",active);
		}

		public PartialViewResult AddPreSetStepToActive (int activeId)
		{
			var ids = new List<int> ();
			foreach (var key in Request.Form.AllKeys) {
				if (key.IndexOf ("pss") != 0)
					continue;
				var selected = (Request.Form [key] == "true,false");
				if (selected) {
					ids.Add (int.Parse (key.Replace ("pss", "")));
				}
			}
			var psss = new PreSetStepService ();
			var pss = psss.GetByIds (ids);
			var service = new ActiveService ();
			var active = service.GetById (activeId);
			foreach (var s in pss) {
				var nas = new ActiveStep () {
					OrderId = (active.Steps.Count+1),
					Content = s.Content,
					Description = s.Description,
					UserDescription = s.Description
				};
				active.Steps.Add (nas);
			}

			service.UpdateSteps (activeId, active.Steps);
			return PartialView ("_ActiveSteps",active);
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
