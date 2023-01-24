using System;
using System.Linq;
using MongoDB.Driver;
using MongoDBDemoApp.Core.Workloads.Posts;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace MongoDBDemoApp.Test;

public sealed class DBTests
{
    private MongoClient _mongoClient;
    private IMongoDatabase _database;
    private IPostRepository _prepo;


    public DBTests()
    {
        _mongoClient = new MongoClient("mongodb://localhost:27017");
        _database = _mongoClient.GetDatabase("mongodb");
        _database.CreateCollectionAsync("TestDb");
    }


    [Fact]
    public async void TestAddPost()
    {
        // Erstellen Sie eines neuen Posts
        Post post = new Post
        {
            Text="Supa Post is des Ko ma sogen",
            Author = "Oliver",
            Published= DateTime.Now,
            Title="Supa Post"
        };

        DBTests dbtest = new DBTests();

        // Fügen Sie den Post in die Datenbank ein
        this._database = dbtest._database ;
        this._mongoClient = dbtest._mongoClient;
        
        await _database.GetCollection<Post>("Post").InsertOneAsync(post);

        // Überprüfung, ob der Post in der Datenbank vorhanden ist
        //var filter = Builders<Post>.Filter.Eq("Author", "Oliver");
        var result = _prepo.GetAllPosts();
        Assert.IsNotNull(result);
        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Result.Count);
        _database.DropCollection("Post");
    }
}