namespace MongoDBDemoApp.Model.Post;

public class PostDto
{
    public string Id { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string Author { get; set; } = default!;
    public string Text { get; set; } = default!;
    public DateTime Published { get; set; }
    public int UpVotes { get; set; }
}