namespace MongoDBDemoApp.Model.Comment;

public sealed class CreateCommentRequest
{
    public string Name { get; set; } = default!;
    public string Mail { get; set; } = default!;
    public string Text { get; set; } = default!;
    public string PostId { get; set; } = default!;
}