using LeoMongo.Database;
using MongoDB.Bson;

namespace MongoDBDemoApp.Core.Workloads.Posts;

public interface IPostRepository : IRepositoryBase
{
    Task<IReadOnlyCollection<Post>> GetAllPosts();
    Task<Post?> GetPostById(ObjectId id);
    Task<Post> AddPost(Post post);
    Task DeletePost(ObjectId id);
    Task<(ObjectId PostId, List<ObjectId>? CommentIds)?> GetPostWithComments(ObjectId postId);
}