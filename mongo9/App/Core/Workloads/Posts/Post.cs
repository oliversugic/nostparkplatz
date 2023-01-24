using LeoMongo.Database;

namespace MongoDBDemoApp.Core.Workloads.Posts;

public sealed class Post : EntityBase
{
    public string Title { get; set; } = default!;
    public string Author { get; set; } = default!;
    public string Text { get; set; } = default!;
    public DateTime Published { get; set; }
    public int UpVotes { get; set; }
}