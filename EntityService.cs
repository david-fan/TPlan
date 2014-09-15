using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Bson.Serialization.Attributes;
using System;
using MongoDB.Bson.Serialization.IdGenerators;


public interface IMongoEntity
{
    ObjectId Id { get; set; }
    int MyId { get; set; }
}
public class MongoEntity : IMongoEntity
{
    [BsonId]
    public ObjectId Id { get; set; }

    public int MyId { get; set; }
}
public interface IEntityService<T> where T : IMongoEntity
{
    void Create(T entity);

    void Delete(int id);

    T GetById(int id);

    void Update(T entity);
}
public abstract class EntityService<T> : IEntityService<T> where T : IMongoEntity
{
    protected readonly MongoConnectionHandler<T> MongoConnectionHandler;

    public virtual void Create(T entity)
    {
        //// Save the entity with safe mode (WriteConcern.Acknowledged)
        entity.MyId = IDHelper.GetNextId(typeof(T).Name);
        var result = this.MongoConnectionHandler.MongoCollection.Save(
            entity,
            new MongoInsertOptions
            {
                WriteConcern = WriteConcern.Acknowledged
            });

        if (!result.Ok)
        {
            //// Something went wrong
        }
    }

    public virtual void Delete(int id)
    {
        var result = this.MongoConnectionHandler.MongoCollection.Remove(
            Query<T>.EQ(e => e.MyId, id),
            RemoveFlags.None,
            WriteConcern.Acknowledged);

        if (!result.Ok)
        {
            //// Something went wrong
        }
    }

    protected EntityService()
    {
        MongoConnectionHandler = new MongoConnectionHandler<T>();
    }

    public virtual T GetById(int id)
    {
        var entityQuery = Query<T>.EQ(e => e.MyId, id);
        return this.MongoConnectionHandler.MongoCollection.FindOne(entityQuery);
    }
	public IEnumerable<T> GetAll()
	{
		var all = this.MongoConnectionHandler.MongoCollection.FindAll();
		return all;
	}
	public IEnumerable<T> GetByIds(IEnumerable<int> ids)
	{
		var query = Query<T>.In(e => e.MyId, ids);
		var results = this.MongoConnectionHandler.MongoCollection.Find(query);
		return results;
	}
    public void Update(T entity)
    {
        //// Not necessary for the example
    }
}