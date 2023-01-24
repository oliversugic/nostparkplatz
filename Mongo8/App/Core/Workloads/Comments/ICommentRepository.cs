using LeoMongo.Database;
using MongoDB.Bson;

namespace MongoDBDemoApp.Core.Workloads.Comments;

public interface ICommentRepository : IRepositoryBase
{
    Task<IReadOnlyCollection<Comment>> GetCommentsForPost(ObjectId postId);
    Task<Comment> AddComment(Comment comment);
    Task<Comment?> GetCommentById(ObjectId id);
    Task<IReadOnlyCollection<Comment>> GetUnapprovedComments();
    Task<bool> ApproveComment(ObjectId id);
    Task<bool> DeleteComment(ObjectId id);
    Task DeleteCommentsByPost(ObjectId postId);
}