using System;
using TeachPlan;

namespace TPlan
{
	public class ActiveDetailModel:Active
	{
		public ActiveDetailModel (Active active)
		{
			this.MyId = active.MyId;
			this.Name = active.Name;
			this.Steps = active.Steps;
			this.Property = active.Property;
			this.Plan_Ids = active.Plan_Ids;

			Phase = new PhaseService ().GetById (active.Phase_Id);
			Subject = new SubjectService ().GetById (active.Subject_Id);
			Content = new ContentService ().GetById (active.Content_Id);
			Form = new FormService ().GetById (active.Form_Id);
			UserInfo = new UserInfoService ().GetById (active.UserInfo_Id);
		}

		public Subject Subject {
			get;
			set;
		}

		public Phase Phase {
			get;
			set;
		}

		public Content Content {
			get;
			set;
		}

		public Form Form {
			get;
			set;
		}

		public UserInfo UserInfo {
			get;
			set;
		}
	}
}

