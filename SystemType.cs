using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeachPlan
{
	public enum TargetType
	{
		ZhishiJineng = 1,
		GuochengFangfa = 2,
		QingganTaidu = 3
	}

	public enum PointType
	{
		Zhongdian = 1,
		Nandian = 2
	}

	public enum ThinkType
	{
		Pingjia = 1,
		Fansi = 2
	}

	public class SystemConst
	{
		public static Dictionary<TargetType, String> TargetTypeName = new Dictionary<TargetType, string> (3);
		public static Dictionary<PointType, String> PointTypeName = new Dictionary<PointType, string> (2);
		public static Dictionary<ThinkType, String> ThinkTypeName = new Dictionary<ThinkType, string> (2);

		public static void Init ()
		{
			TargetTypeName.Add (TargetType.ZhishiJineng, "知识技能");
			TargetTypeName.Add (TargetType.GuochengFangfa, "过程方法");
			TargetTypeName.Add (TargetType.QingganTaidu, "情感态度");
			PointTypeName.Add (PointType.Nandian, "难点");
			PointTypeName.Add (PointType.Zhongdian, "重点");
			ThinkTypeName.Add (ThinkType.Fansi, "反思");
			ThinkTypeName.Add (ThinkType.Pingjia, "评价");
		}
	}
}
