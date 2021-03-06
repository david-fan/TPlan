﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Models;

namespace TeachPlan.Controllers
{
    public class KnowledgeController : Controller
    {

        public ActionResult Index()
        {
            var catgorys = GetByParent(null);
            return View(catgorys);
        }

        public ActionResult Category()
        {
            return View();
        }

        public PartialViewResult LastCommentKnowledge()
        {
            var service = new KnowledgeService();
            var enumer = service.GetByIds(new int[] { 1, 2 });
            return PartialView("_KnowledgeList", enumer);
        }

        public PartialViewResult LastViewKnowledge()
        {
            var service = new KnowledgeService();
            var enumer = service.GetByIds(new int[] { 1, 2 });
            return PartialView("_KnowledgeList", enumer);
        }

        public PartialViewResult LastAddKnowledge()
        {
            var service = new KnowledgeService();
            var enumer = service.GetLastCreate(10);
            return PartialView("_KnowledgeList", enumer);
        }

		public PartialViewResult KnowledgeComments(int knowledgeId)
		{
			return PartialView ("_Comments", GetCommentByKnowledge(knowledgeId));
		}

		public Comment[] GetCommentByKnowledge(int knowledgeId)
        {
            var service = new CommentService();
			var enumer = service.GetKnowledgeComments(knowledgeId);
            return enumer.ToArray();
        }

        [HttpPost]
		public JsonResult AddComment(Comment comment)
        {
            var service = new CommentService();
            service.Create(comment);
            return Json(true);
        }

		[HttpGet]
		public PartialViewResult AddComment(int knowledgeId)
		{
			var comment = new Comment();
			comment.To_Type = (int)ToType.Knowledge;
			comment.To_Id = knowledgeId;
			return PartialView ("_AddComment", comment);
		}
			
		[HttpGet]
		public PartialViewResult AddKnowledge()
		{
			return PartialView ("_AddKnowledge",new Knowledge());
		}
			
		[HttpPost]
		[ValidateInput(false)]
		public JsonResult AddKnowledge(Knowledge knowledge)
		{
			var service = new KnowledgeService ();
			//FormCollection collection = new FormCollection(Request.Unvalidated().Form);

			service.Create (knowledge);
			return Json (true);
		}

		public ViewResult KnowledgeDetail(int id)
		{
			var service = new KnowledgeService ();
			var knowlege = service.GetById (id);
			return View ("_KnowledgeDetail", knowlege);
		}

        public KnowledgeCategory AddCategory(KnowledgeCategory parent)
        {
            var category = new KnowledgeCategory();
            category.ParentCategory_Id = parent.MyId;
            var service = new KnowledgeCategoryService();
            service.Create(category);
            return category;
        }

        public KnowledgeCategory[] GetByParent(KnowledgeCategory parent)
        {
            var service = new KnowledgeCategoryService();
            IEnumerable<KnowledgeCategory> enumer;
            if (parent == null)
                enumer = service.GetChildCategory(0);
            else
                enumer = service.GetChildCategory(parent.MyId);
            return enumer.ToArray();
        }

        public Knowledge[] GetKnowledgeByCategory(KnowledgeCategory category)
        {
            var enumer = (new KnowledgeService()).GetKnowledgesByCategoryId(category.MyId);
            return enumer.ToArray();
        }

        public PartialViewResult CategoryTree()
        {
            var service = new KnowledgeCategoryService();
            List<KnowledgeCategoryTreeModel> model = new List<KnowledgeCategoryTreeModel>();
            var tops = service.GetChildCategory(0);
            foreach (var top in tops)
            {
                var t = new KnowledgeCategoryTreeModel(top);
                t.ChildCategorys = service.GetChildCategory(t.MyId);
                model.Add(t);
            }
            return PartialView("_CategoryTree", model);
        }
    }
}
