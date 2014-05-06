using System;
using System.Collections;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace TeachPlan
{
	public class ID : MongoEntity
	{
		public string Name { get; set; }

		public int CurrentID { get; set; }
	}

	public class Plan : MongoEntity
	{
		public string Desc { get; set; }

		public string Name { get; set; }

		public string Author { get; set; }

		public string Organiser { get; set; }

		public int Refer { get; set; }

		public bool Public { get; set; }

		public string ClassName { get; set; }

		public DateTime CreateDate { get; set; }

		public int Grade_Id { get; set; }

		public int Textbook_Id { get; set; }

		public int UserInfo_Id { get; set; }

		public int Subject_Id { get; set; }

		public List<UploadFile> Files {
			get;
			set;
		}

		public List<Target> Targets {
			get;
			set;
		}

		public List<Point> Points {
			get;
			set;
		}

		public List<Think> Thinks {
			get;
			set;
		}
	}
	#region Plan's Property Class
	public class Point
	{
		[BsonIgnoreAttribute]
		public int planId{ get; set; }

		public int Type { get; set; }

		public string Content { get; set; }

		public string Method { get; set; }
	}
	public class UploadFile
	{
		[BsonIgnoreAttribute]
		public int planId{ get; set;}
		public string fileName{ get; set;}
		public string fileDesc{ get; set;}
		public string filePath{ get; set; }
	}
	public class Think
	{
		[BsonIgnoreAttribute]
		public int planId{ get; set; }

		public int Type{ get; set; }

		public string Content { get; set; }
	}

	public class Target
	{
		[BsonIgnoreAttribute]
		public int planId{ get; set; }

		public int Type { get; set; }

		public string Baseic { get; set; }

		public string Advance { get; set; }
	}
	#endregion
	/*
	public class ActiveComment : MongoEntity
	{
		public string Content { get; set; }

		public int Active_Id { get; set; }

		public int UserInfo_Id { get; set; }
	}
*/
	public class Active : MongoEntity
	{
		public string Name { get; set; }

		public string Property { get; set; }

		public string Condition { get; set; }
		//public string Step { get; set; }
		public List<String> Steps {
			get;
			set;
		}

		public int Plan_Id { get; set; }

		public int Subject_Id { get; set; }

		public int Content_Id { get; set; }

		public int Phase_Id { get; set; }

		public int Form_Id { get; set; }

		public int UserInfo_Id { get; set; }
	}

	public class Character : MongoEntity
	{
		public int Type { get; set; }

		public int Plan_Id { get; set; }
	}

	public class Content : MongoEntity
	{
		public string Name { get; set; }
	}

	public class Form : MongoEntity
	{
		public string Name { get; set; }
	}

	public class Grade : MongoEntity
	{
		public string Name { get; set; }
	}

	public class KnowledgeCategory : MongoEntity
	{
		public string CategoryName { get; set; }

		public int ParentCategory_Id { get; set; }

		public int UserInfo_Id { get; set; }
	}

	public class Comment : MongoEntity
	{
		public string Content { get; set; }

		public DateTime CreateDate { get; set; }

		public int To_Id { get; set; }

		public int To_Type { get; set; }

		public int UserInfo_Id { get; set; }
	}

	public class ViewLog : MongoEntity
	{
		public DateTime CreateDate { get; set; }

		public int To_Id { get; set; }

		public int To_Type { get; set; }

		public int UserInfo_Id { get; set; }
	}

	public class Knowledge : MongoEntity
	{
		public string Content { get; set; }

		public DateTime CreateDate { get; set; }

		public string Title { get; set; }

		public int KnowledgeCategory_Id { get; set; }

		public int UserInfo_Id { get; set; }
	}

	public class KnowledgeViewLog : MongoEntity
	{
		public DateTime CreateDate { get; set; }

		public int Knowledge_Id { get; set; }

		public int UserInfo_Id { get; set; }
	}

	public class Method : MongoEntity
	{
		public string Content { get; set; }

		public int Point_Id { get; set; }
	}

	public class Phase : MongoEntity
	{
		public string Name { get; set; }
	}

	public class Ready : MongoEntity
	{
		public int Type { get; set; }

		public int Plan_Id { get; set; }
	}

	public class Subject : MongoEntity
	{
		public string Name { get; set; }
	}

	public class Textbook : MongoEntity
	{
		public string Name { get; set; }
	}

	public class UserInfo : MongoEntity
	{
		public string Name { get; set; }

		public string PWD { get; set; }
	}
}