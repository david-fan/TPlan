using System.Collections;
using System.Collections.Generic;
using TeachPlan;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

public class PlanService : EntityService<Plan>
{
    public void UpdatePlan(int planId, List<Target> targets)
    {
        var query = Query<Plan>.EQ(e => e.MyId, planId);
        var update = Update<Plan>.Set(e => e.Targets, targets);
        this.MongoConnectionHandler.MongoCollection.Update(query, update, UpdateFlags.Upsert);
    }
    public void UpdatePlan(int planId,List<Point> points)
    {
        var query = Query<Plan>.EQ(e => e.MyId, planId);
        var update = Update<Plan>.Set(e => e.Points, points);
        this.MongoConnectionHandler.MongoCollection.Update(query, update, UpdateFlags.Upsert);
    }
	public void UpdatePlan(int planId,List<UploadFile> points)
	{
		var query = Query<Plan>.EQ(e => e.MyId, planId);
		var update = Update<Plan>.Set(e => e.Files, points);
		this.MongoConnectionHandler.MongoCollection.Update(query, update, UpdateFlags.Upsert);
	}
    public void UpdatePlan(int planId, List<Think> thinks)
    {
        var query = Query<Plan>.EQ(e => e.MyId, planId);
        var update = Update<Plan>.Set(e => e.Thinks, thinks);
        this.MongoConnectionHandler.MongoCollection.Update(query, update, UpdateFlags.Upsert);
    }

    public IEnumerable<Plan> GetLastCreate(int limit)
    {
        var results = this.MongoConnectionHandler.MongoCollection.FindAllAs<Plan>()
            .SetSortOrder(SortBy<Plan>.Descending(g => g.CreateDate))
            .SetLimit(limit);
        return results;
    }
}

public class ContentService : EntityService<Content>
{
//    public IEnumerable<Content> GetAll()
//    {
//        var comments = this.MongoConnectionHandler.MongoCollection.FindAll();
//        return comments;
//    }
}

public class FormService : EntityService<Form>
{
//    public IEnumerable<Form> GetAll()
//    {
//        var comments = this.MongoConnectionHandler.MongoCollection.FindAll();
//        return comments;
//    }
}

public class PhaseService : EntityService<Phase>
{
//    public IEnumerable<Phase> GetAll()
//    {
//        var comments = this.MongoConnectionHandler.MongoCollection.FindAll();
//        return comments;
//    }
}

public class SubjectService : EntityService<Subject>
{
//    public IEnumerable<Subject> GetAll()
//    {
//        var comments = this.MongoConnectionHandler.MongoCollection.FindAll();
//        return comments;
//    }
}

public class TextbookService : EntityService<Textbook>
{
//    public IEnumerable<Textbook> GetAll()
//    {
//        var comments = this.MongoConnectionHandler.MongoCollection.FindAll();
//        return comments;
//    }
}

public class GradeService : EntityService<Grade>
{
//    public IEnumerable<Grade> GetAll()
//    {
//        var comments = this.MongoConnectionHandler.MongoCollection.FindAll();
//        return comments;
//    }
}
	
public class ActiveService : EntityService<Active>
{
    public IEnumerable<Active> GetByPlan(int planId)
    {
		var query = Query<Active>.EQ(e => e.Plan_Id, planId);
        var results = this.MongoConnectionHandler.MongoCollection.Find(query);
        return results;
    }

	public void UpdateSteps(int activeId,List<ActiveStep>  steps)
	{
		var query = Query<Active>.EQ(e => e.MyId, activeId);
		var update = Update<Active>.Set(e => e.Steps, steps);
		this.MongoConnectionHandler.MongoCollection.Update(query, update, UpdateFlags.Upsert);
	}

	public IEnumerable<Active> GetByUserId(int userId,int formId,int subjectId,int phaseId,int contentId)
    {
		var queries = new List<IMongoQuery> ();
		if (userId > 0)
			queries.Add (Query<Active>.EQ (e => e.UserInfo_Id, userId));
		if (formId > 0)
			queries.Add (Query<Active>.EQ (e => e.Form_Id, formId));
		if (subjectId > 0)
			queries.Add (Query<Active>.EQ (e => e.Subject_Id, subjectId));
		if (phaseId > 0)
			queries.Add (Query<Active>.EQ (e => e.Phase_Id, phaseId));
		if (contentId > 0)
			queries.Add (Query<Active>.EQ (e => e.Content_Id, contentId));
		var results = this.MongoConnectionHandler.MongoCollection.Find(Query.And(queries));
        return results;
    }
}

public class KnowledgeCategoryService : EntityService<KnowledgeCategory>
{
    public IEnumerable<KnowledgeCategory> GetChildCategory(int categoryId)
    {
        var query = Query<KnowledgeCategory>.EQ(e => e.ParentCategory_Id, categoryId);
        var comments = this.MongoConnectionHandler.MongoCollection.Find(query);
        return comments;
    }
}

public enum ToType
{
    Plan = 1,
	Knowledge = 2,
	Active=3
}

public class ViewlogService : EntityService<ViewLog>
{
    public IEnumerable<ViewLog> GetLastCreate(ToType type, int limit)
    {
        var query = Query<ViewLog>.EQ(e => e.To_Type, (int)type);
        var results = this.MongoConnectionHandler.MongoCollection.FindAs<ViewLog>(query)
            .SetSortOrder(SortBy<ViewLog>.Descending(g => g.CreateDate))
            .SetLimit(limit);
        return results;
    }

    public IEnumerable<ViewLog> GetKnowledgeViewlogs(int knowledgeId)
    {
        var query = Query.And(Query<ViewLog>.EQ(e => e.To_Id, knowledgeId), Query<ViewLog>.EQ(e => e.To_Type, (int)ToType.Knowledge));
        var comments = this.MongoConnectionHandler.MongoCollection.Find(query);
        return comments;
    }
    public IEnumerable<ViewLog> GetPlanViewlogs(int planId)
    {
        var query = Query.And(Query<ViewLog>.EQ(e => e.To_Id, planId), Query<ViewLog>.EQ(e => e.To_Type, (int)ToType.Plan));
        var comments = this.MongoConnectionHandler.MongoCollection.Find(query);
        return comments;
    }

}

public class CommentService : EntityService<Comment>
{
    public IEnumerable<Comment> GetLastCreate(ToType type, int limit)
    {
        var query = Query<Comment>.EQ(e => e.To_Type, (int)type);
        var results = this.MongoConnectionHandler.MongoCollection.FindAs<Comment>(query)
            .SetSortOrder(SortBy<Comment>.Descending(g => g.CreateDate))
            .SetLimit(limit);
        return results;
    }

    public IEnumerable<Comment> GetKnowledgeComments(int knowledgeId)
    {
        var query = Query.And(Query<Comment>.EQ(e => e.To_Id, knowledgeId), Query<Comment>.EQ(e => e.To_Type, (int)ToType.Knowledge));
        var comments = this.MongoConnectionHandler.MongoCollection.Find(query);
        return comments;
    }
    public IEnumerable<Comment> GetPlanComments(int planId)
    {
        var query = Query.And(Query<Comment>.EQ(e => e.To_Id, planId), Query<Comment>.EQ(e => e.To_Type, (int)ToType.Plan));
        var comments = this.MongoConnectionHandler.MongoCollection.Find(query);
        return comments;
    }

}
public class PreSetStepService : EntityService<PreSetStep>
{

}

public class KnowledgeService : EntityService<Knowledge>
{
    public IEnumerable<Knowledge> GetLastCreate(int limit)
    {
        var knowledges = this.MongoConnectionHandler.MongoCollection.FindAllAs<Knowledge>()
            .SetSortOrder(SortBy<Knowledge>.Descending(g => g.CreateDate))
            .SetLimit(limit);
        return knowledges;
    }

    public IEnumerable<Knowledge> GetKnowledgesByCategoryId(int categoryId)
    {
        var query = Query<Knowledge>.EQ(e => e.KnowledgeCategory_Id, categoryId);
        var knowledges = this.MongoConnectionHandler.MongoCollection.Find(query);
        return knowledges;
    }
}

public class UserInfoService : EntityService<UserInfo>
{
    public UserInfo Login(string name, string pwd)
    {
        var query = Query.And(Query<UserInfo>.EQ(u => u.Name, name), Query<UserInfo>.EQ(u => u.PWD, pwd));
        var user = this.MongoConnectionHandler.MongoCollection.FindOne(query);
        return user;
    }
}