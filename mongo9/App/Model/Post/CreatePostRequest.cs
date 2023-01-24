namespace MongoDBDemoApp.Model.Post;

public sealed class CreatePostRequest
{
    public string Title { get; set; } = default!;
    public string Author { get; set; } = default!;
    public string Text { get; set; } = default!;
}