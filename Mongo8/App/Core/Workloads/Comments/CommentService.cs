using MongoDB.Bson;
using MongoDBDemoApp.Core.Util;
using MongoDBDemoApp.Core.Workloads.Posts;

namespace MongoDBDemoApp.Core.Workloads.Comments;

public sealed class CommentService : ICommentService
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ICommentRepository _repository;

    public CommentService(IDateTimeProvider dateTimeProvider, ICommentRepository repository)
    {
        _dateTimeProvider = dateTimeProvider;
        _repository = repository;
    }

    public Task<IReadOnlyCollection<Comment>> GetCommentsForPost(Post post) =>
        _repository.GetCommentsForPost(post.Id);

    public Task<Comment> AddComment(Post post, string name, string mail, string text)
    {
        var comment = new Comment
        {
            Created = _dateTimeProvider.Now,
            PostId = post.Id,
            Text = text,
            Mail = mail,
            Name = name,
            Approved = false
        };
        return _repository.AddComment(comment);
    }

    public Task<Comment?> GetCommentById(ObjectId id) => _repository.GetCommentById(id);
    public Task<IReadOnlyCollection<Comment>> GetUnapprovedComments() => _repository.GetUnapprovedComments();
    public Task<bool> DeleteComment(Comment comment) => _repository.DeleteComment(comment.Id);
    public Task<bool> ApproveComment(Comment comment)
    {
        return comment.Approved ? Task.FromResult(true) : _repository.ApproveComment(comment.Id);
    }
}