namespace MongoDBDemoApp.Model.Comment;

public sealed class CommentDto
{
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Mail { get; set; } = default!;
    public string Text { get; set; } = default!;
    public DateTime Created { get; set; }
}