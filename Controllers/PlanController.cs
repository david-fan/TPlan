using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.IO;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Models;
using TPlan;

namespace TeachPlan.Controllers
{
	public class PlanController : Controller
	{
		public ActionResult Index ()
		{
			return View ();

            
		}

		/// <summary>
		/// 所有学科
		/// </summary>
		/// <returns></returns>
		public static IEnumerable<SelectListItem> getAllSubjects ()
		{
			var grades = new List<SelectListItem> ();
			var service = new SubjectService ();
			var g = service.GetAll ().GetEnumerator ();
			while (g.MoveNext ()) {
				var sli = new SelectListItem ();
				sli.Text = g.Current.Name;
				sli.Value = g.Current.MyId.ToString ();
				//if (select != null && select.Id == g.Current.Id)
				//    sli.Selected = true;
				grades.Add (sli);
			}
			return grades;
		}

		/// <summary>
		/// 所有教材
		/// </summary>
		/// <returns></returns>
		public static IEnumerable<SelectListItem> getAllTextbooks ()
		{
			var grades = new List<SelectListItem> ();
			var service = new TextbookService ();
			var g = service.GetAll ().GetEnumerator ();
			while (g.MoveNext ()) {
				var sli = new SelectListItem ();
				sli.Text = g.Current.Name;
				sli.Value = g.Current.MyId.ToString ();
				//if (select != null && select.Id == g.Current.Id)
				//    sli.Selected = true;
				grades.Add (sli);
			}
			return grades;
		}

		/// <summary>
		/// 所有目标层次
		/// </summary>
		/// <returns></returns>
		public static IEnumerable<SelectListItem> getAllPhases ()
		{
			var grades = new List<SelectListItem> ();
			var service = new PhaseService ();
			var g = service.GetAll ().GetEnumerator ();
			while (g.MoveNext ()) {
				var sli = new SelectListItem ();
				sli.Text = g.Current.Name;
				sli.Value = g.Current.MyId.ToString ();
				//if (select != null && select.Id == g.Current.Id)
				//    sli.Selected = true;
				grades.Add (sli);
			}
			return grades;
		}

		/// <summary>
		/// 所有年级
		/// </summary>
		/// <returns></returns>
		public static IEnumerable<SelectListItem> getAllGrades ()
		{
			var grades = new List<SelectListItem> ();
			var service = new GradeService ();
			var g = service.GetAll ().GetEnumerator ();
			while (g.MoveNext ()) {
				var sli = new SelectListItem ();
				sli.Text = g.Current.Name;
				sli.Value = g.Current.MyId.ToString ();
				//if (select != null && select.Id == g.Current.Id)
				//    sli.Selected = true;
				grades.Add (sli);
			}
			return grades;
		}

		/// <summary>
		/// 所有教学内容
		/// </summary>
		/// <returns></returns>
		public static IEnumerable<SelectListItem> getAllContents ()
		{
			var grades = new List<SelectListItem> ();
			var service = new ContentService ();
			var g = service.GetAll ().GetEnumerator ();
			while (g.MoveNext ()) {
				var sli = new SelectListItem ();
				sli.Text = g.Current.Name;
				sli.Value = g.Current.MyId.ToString ();
				//if (select != null && select.Id == g.Current.Id)
				//    sli.Selected = true;
				grades.Add (sli);
			}
			return grades;
		}

		/// <summary>
		/// 所有组织形式
		/// </summary>
		/// <returns></returns>
		public static IEnumerable<SelectListItem> getAllForms ()
		{
			var grades = new List<SelectListItem> ();
			var service = new FormService ();
			var g = service.GetAll ().GetEnumerator ();
			while (g.MoveNext ()) {
				var sli = new SelectListItem ();
				sli.Text = g.Current.Name;
				sli.Value = g.Current.MyId.ToString ();
				//if (select != null && select.Id == g.Current.Id)
				//    sli.Selected = true;
				grades.Add (sli);
			}
			return grades;
		}

		public static IEnumerable<SelectListItem> getAllThinkTypeName ()
		{
			var grades = new List<SelectListItem> ();
			//var service = new ContentService();
			var g = SystemConst.ThinkTypeName.GetEnumerator ();
			while (g.MoveNext ()) {
				var sli = new SelectListItem ();
				sli.Text = g.Current.Value;
				sli.Value = ((int)g.Current.Key).ToString ();
				grades.Add (sli);
			}
			return grades;
		}

		public JsonResult RemoveActive (int acitveId)
		{
			return Json (true);
		}

		public ViewResult Detail (int id)
		{
			var service = new PlanService ();
			var plan = service.GetById (id);

			var model = new PlanDetail (plan);
			return View ("_Detail", model);
		}

		public PartialViewResult AddPlan ()
		{
			return PartialView ("_AddPlan");
		}

		[HttpPost]
		public JsonResult AddPlan (Plan model)
		{
			var service = new PlanService ();
			model.CreateDate = DateTime.Now;
			model.UserInfo_Id = int.Parse (Session ["user"].ToString ());
			//model.UserInfo = db.UserInfoSet.First(m => m.Id == uid);
			//model.Grade = db.GradeSet.First(m => m.Id == model.Grade.Id);
			//model.Subject = db.SubjectSet.First(m => m.Id == model.Subject.Id);
			//model.Textbook = db.TextbookSet.First(m => m.Id == model.Textbook.Id);
			service.Create (model);
			return Json (true);
		}

		public PartialViewResult LastCommentPlans ()
		{
			var cids = (new CommentService ()).GetLastCreate (ToType.Plan, 10);
			var pids = from p in cids
			           select p.To_Id;
			var enumer = (new PlanService ()).GetByIds (pids);
			return PartialView ("_PlanList", enumer);
		}

		public PartialViewResult LastViewPlans ()
		{
			var cids = (new ViewlogService ()).GetLastCreate (ToType.Plan, 10);
			var pids = from p in cids
			           select p.To_Id;
			var enumer = (new PlanService ()).GetByIds (pids);
			return PartialView ("_PlanList", enumer);
		}

		public PartialViewResult LastAddPlans ()
		{
			var service = new PlanService ();
			var enumer = service.GetLastCreate (10);
			return PartialView ("_PlanList", enumer);
		}

		[HttpGet]
		public PartialViewResult AddPoint (int planId)
		{
			var model = new Point ();
			model.planId = planId;
			return PartialView ("_AddPoint", model);
		}

		[HttpPost]
		public JsonResult AddPoint (Point model)
		{
			var service = new PlanService ();
			var plan = service.GetById (model.planId);
			if (plan.Points == null)
				plan.Points = new List<Point> ();
			plan.Points.Add (model);
			service.UpdatePlan (model.planId, plan.Points);
			return Json (true);
		}

		[HttpGet]
		public PartialViewResult AddTarget (int planId)
		{
			var model = new Target ();
			model.planId = planId;
			return PartialView ("_AddTarget", model);
		}

		[HttpPost]
		public JsonResult AddTarget (Target model)
		{
			var service = new PlanService ();
			var plan = service.GetById (model.planId);
			if (plan.Targets == null)
				plan.Targets = new List<Target> ();
			plan.Targets.Add (model);
			service.UpdatePlan (model.planId, plan.Targets);
			return Json (true);
		}

		[HttpGet]
		public PartialViewResult AddThink (int planId)
		{
			var model = new Think ();
			model.planId = planId;
			return PartialView ("_AddThink", model);
		}

		[HttpPost]
		public JsonResult AddTarget (Think model)
		{
			var service = new PlanService ();
			var plan = service.GetById (model.planId);
			if (plan.Thinks == null)
				plan.Thinks = new List<Think> ();
			plan.Thinks.Add (model);
			service.UpdatePlan (model.planId, plan.Thinks);
			return Json (true);
		}

		[HttpGet]
		public PartialViewResult UploadFile (int planId)
		{
			var model = new UploadFile ();
			model.planId = planId;
			return PartialView ("_UploadFile");
		}

		[HttpPost]
		public JsonResult UploadFile (UploadFile model)
		{
			if (Request.Files.Count > 0) {
				var file = Request.Files [0];
				if (file != null && file.ContentLength > 0) {
					var path = Server.MapPath ("~/plan_" + model.planId+ "/upload/");
					var fileExt = file.FileName.Substring (file.FileName.IndexOf ("."));
					var fileName = DateTime.Now.ToString ("yyMMddhhmmssfff");
					if (!Directory.Exists (path)) {
						Directory.CreateDirectory (path);
					}
					var savePath = Path.Combine (path, fileName) + fileExt;
					file.SaveAs (savePath);
					model.filePath = fileName + fileExt;
					var service = new PlanService ();
					var plan = service.GetById (model.planId);
					if (plan.Files == null)
						plan.Files = new List<UploadFile> ();
					plan.Files.Add (model);
					service.UpdatePlan (model.planId, plan.Files);
				}
			}
			return Json (true);
		}
	}
}
