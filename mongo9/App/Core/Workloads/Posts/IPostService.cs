using MongoDB.Bson;

namespace MongoDBDemoApp.Core.Workloads.Posts;

public interface IPostService
{
    Task<IReadOnlyCollection<Post>> GetAllPosts();
    Task<Post?> GetPostById(ObjectId id);
    Task<Post> AddPost(string title, string author, string text);
    Task DeletePost(ObjectId id);
}