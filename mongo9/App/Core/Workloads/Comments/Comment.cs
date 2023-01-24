using LeoMongo.Database;
using MongoDB.Bson;

namespace MongoDBDemoApp.Core.Workloads.Comments;

public sealed class Comment : EntityBase
{
    public string Name { get; set; } = default!;
    public string Mail { get; set; } = default!;
    public string Text { get; set; } = default!;
    public DateTime Created { get; set; }
    public ObjectId PostId { get; set; }
    public bool Approved { get; set; }
}