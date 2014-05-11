using System;
using TeachPlan;
using System.Collections;
using System.Collections.Generic;

namespace Models
{

    public class PlanDetail : Plan
    {
        public PlanDetail(Plan plan)
        {
            this.Author = plan.Author;
            this.ClassName = plan.ClassName;
            this.CreateDate = plan.CreateDate;
            this.MyId = plan.MyId;
            this.Id = plan.Id;
            this.Name = plan.Name;
            this.Organiser = plan.Organiser;
            this.Desc = plan.Desc;
            this.Public = plan.Public;

            this.Subject = (new SubjectService()).GetById(plan.Subject_Id);
            this.Grade = (new GradeService()).GetById(plan.Grade_Id);
            this.Textbook = (new TextbookService()).GetById(plan.Textbook_Id);
            this.UserInfo = (new UserInfoService()).GetById(plan.UserInfo_Id);

            this.Actives = (new ActiveService()).GetByPlan(plan.MyId);

            if (plan.Targets == null)
                this.Targets = new List<Target>();
            else
                this.Targets = plan.Targets;

            if (plan.Points == null)
                this.Points = new List<Point>();
            else
                this.Points = plan.Points;

            if (plan.Thinks == null)
                this.Thinks = new List<Think>();
            else
                this.Thinks = plan.Thinks;

			if (plan.Files == null)
				this.Files = new List<UploadFile>();
			else
				this.Files = plan.Files;
		}

        public Subject Subject
        {
            get;
            set;
        }

        public Grade Grade
        {
            get;
            set;
        }

        public Textbook Textbook
        {
            get;
            set;
        }

        public UserInfo UserInfo
        {
            get;
            set;
        }

        public IEnumerable<Active> Actives
        {
            get;
            set;
        }


    }

}