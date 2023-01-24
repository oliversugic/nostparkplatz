using MongoDB.Bson;
using MongoDBDemoApp.Core.Util;
using MongoDBDemoApp.Core.Workloads.Comments;

namespace MongoDBDemoApp.Core.Workloads.Posts;

public sealed class PostService : IPostService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ILogger<PostService> _logger;
    private readonly IPostRepository _repository;

    public PostService(IDateTimeProvider dateTimeProvider, IPostRepository repository,
        ICommentRepository commentRepository, ILogger<PostService> logger)
    {
        _dateTimeProvider = dateTimeProvider;
        _repository = repository;
        _commentRepository = commentRepository;
        _logger = logger;
    }

    public Task<IReadOnlyCollection<Post>> GetAllPosts() => _repository.GetAllPosts();

    public Task<Post?> GetPostById(ObjectId id) => _repository.GetPostById(id);

    public Task<Post> AddPost(string title, string author, string text)
    {
        var post = new Post
        {
            Author = author,
            Published = _dateTimeProvider.Now,
            Text = text,
            Title = title,
            UpVotes = 0
        };
        return _repository.AddPost(post);
    }

    public async Task DeletePost(ObjectId id)
    {
        (ObjectId PostId, List<ObjectId>? CommentIds)? postWithComments =
            await _repository.GetPostWithComments(id);
        if (postWithComments == null)
        {
            throw new ArgumentException(nameof(id));
        }

        List<ObjectId>? comments = postWithComments.Value.CommentIds;
        if (comments is { Count: > 0 })
        {
            _logger.LogInformation($"Deleting {comments.Count} comment(s) together with post {id}.");
            await _commentRepository.DeleteCommentsByPost(id);
        }

        await _repository.DeletePost(postWithComments.Value.PostId);
    }
}