using MongoDB.Bson;
using MongoDBDemoApp.Core.Workloads.Posts;

namespace MongoDBDemoApp.Core.Workloads.Comments;

public interface ICommentService
{
    Task<IReadOnlyCollection<Comment>> GetCommentsForPost(Post post);
    Task<Comment> AddComment(Post post, string name, string mail, string text);
    Task<Comment?> GetCommentById(ObjectId id);
    Task<IReadOnlyCollection<Comment>> GetUnapprovedComments();
    Task<bool> ApproveComment(Comment comment);
    Task<bool> DeleteComment(Comment comment);
}