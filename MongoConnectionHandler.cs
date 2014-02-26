using TeachPlan;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
public class MongoConnectionHandler<T> where T : IMongoEntity
{
    public MongoCollection<T> MongoCollection { get; private set; }

    public MongoConnectionHandler()
    {
        //// Get a reference to the collection object from the Mongo database object
        //// The collection name is the type converted to lowercase + "s"
        MongoCollection = (new DataBase()).db.GetCollection<T>(typeof(T).Name + "Set");
    }
}



public class DataBase
{
    public MongoDatabase db
    {
        get
        {
            const string connectionString = "mongodb://localhost";

            //// Get a thread-safe client object by using a connection string
            var mongoClient = new MongoClient(connectionString);

            //// Get a reference to a server object from the Mongo client object
            var mongoServer = mongoClient.GetServer();

            //// Get a reference to the "retrogamesweb" database object 
            //// from the Mongo server object
            const string databaseName = "teachplan";
            return mongoServer.GetDatabase(databaseName);
        }
    }
}

public class IDHelper
{
    public static int GetNextId(string classType)
    {
        var result = (new DataBase()).db.GetCollection<ID>("IDSet").FindAndModify(
            Query<ID>.EQ(e => e.Name, classType), 
            SortBy<ID>.Ascending(e => e.Name), 
            Update<ID>.Inc(e => e.CurrentID, 1), true,true);
        return result.GetModifiedDocumentAs<ID>().CurrentID;

    }
}