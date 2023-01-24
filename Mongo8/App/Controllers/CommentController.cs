using AutoMapper;
using LeoMongo.Transaction;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDBDemoApp.Core.Workloads.Comments;
using MongoDBDemoApp.Core.Workloads.Posts;
using MongoDBDemoApp.Model.Comment;

namespace MongoDBDemoApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class CommentController : ControllerBase
{
    private readonly ILogger<CommentController> _logger;
    private readonly IMapper _mapper;
    private readonly IPostService _postService;
    private readonly ICommentService _service;
    private readonly ITransactionProvider _transactionProvider;

    public CommentController(IMapper mapper, ICommentService service, ITransactionProvider transactionProvider,
        IPostService postService, ILogger<CommentController> logger)
    {
        _mapper = mapper;
        _service = service;
        _transactionProvider = transactionProvider;
        _postService = postService;
        _logger = logger;
    }

    /// <summary>
    ///     Returns all comments for the post with the given id.
    /// </summary>
    /// <param name="postId">id of an existing post</param>
    /// <returns>List of comments, may be empty</returns>
    [HttpGet]
    [Route("post")]
    public async Task<ActionResult<IReadOnlyCollection<CommentDto>>> GetCommentsForPost(string postId)
    {
        Post? post;
        if (string.IsNullOrWhiteSpace(postId) ||
            (post = await _postService.GetPostById(new ObjectId(postId))) == null)
        {
            return BadRequest();
        }

        IReadOnlyCollection<Comment> comments = await _service.GetCommentsForPost(post);
        return Ok(_mapper.Map<List<CommentDto>>(comments));
    }

    /// <summary>
    ///     Returns all unapproved comments.
    /// </summary>
    /// <returns>List of unapproved comments, may be empty</returns>
    [HttpGet]
    [Route("unapproved")]
    public async Task<ActionResult<IReadOnlyCollection<CommentDto>>> GetUnapprovedComments()
    {
        IReadOnlyCollection<Comment> comments = await _service.GetUnapprovedComments();
        return Ok(_mapper.Map<List<CommentDto>>(comments));
    }

    /// <summary>
    ///     Sets an existing comment as approved.
    /// </summary>
    /// <param name="id">The id of the comment</param>
    [HttpPut]
    [Route("approve")]
    public async Task<IActionResult> ApproveComment([FromBody] ApproveCommentRequest request)
    {
        string? id = request?.Id;
        Comment? comment;
        if (string.IsNullOrWhiteSpace(id)
            || (comment = await _service.GetCommentById(new ObjectId(id))) == null)
        {
            return BadRequest();
        }

        if (!await _service.ApproveComment(comment))
        {
            _logger.LogInformation($"Duplicate approval of comment {id}");
        }

        return Ok();
    }

    /// <summary>
    ///     Deletes a comment.
    /// </summary>
    /// <param name="id">The id of the comment</param>
    [HttpDelete]
    public async Task<IActionResult> DeleteComment(string id)
    {
        // TODO
        if (string.IsNullOrWhiteSpace(id))
        {
            return BadRequest();
        }

        using var transaction = await _transactionProvider.BeginTransaction();

        Comment? getCommentById = await _service.GetCommentById(ObjectId.Parse(id));

        if (getCommentById == null)
        {
            await transaction.RollbackAsync();
            return BadRequest();
        }

        await _service.DeleteComment(getCommentById);
        await transaction.CommitAsync();
        return Ok();
    }

    /// <summary>
    ///     Returns the comment identified by the given id if it exists.
    /// </summary>
    /// <param name="id">existing comment id</param>
    /// <returns>a comment</returns>
    [HttpGet]
    public async Task<ActionResult<CommentDto>> GetById(string id)
    {
        Comment? comment;
        if (string.IsNullOrWhiteSpace(id)
            || (comment = await _service.GetCommentById(new ObjectId(id))) == null)
        {
            return BadRequest();
        }

        return Ok(_mapper.Map<CommentDto>(comment));
    }

    /// <summary>
    ///     Creates a new comment.
    /// </summary>
    /// <param name="request">Data for the new comment</param>
    /// <returns>the created comment if successful</returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCommentRequest request)
    {
        // TODO
        if (string.IsNullOrWhiteSpace(request.Name)
    || string.IsNullOrWhiteSpace(request.Text)
    || string.IsNullOrWhiteSpace(request.Mail))
        {
            return BadRequest();
        }

        using var transaction = await _transactionProvider.BeginTransaction();
        Post? getPostById = await _postService.GetPostById(ObjectId.Parse(request.PostId));
        if (getPostById == null)
        {
            await transaction.RollbackAsync();
            return BadRequest();
        }
        else
        {
            Comment comment = await _service.AddComment(getPostById, request.Name, request.Mail, request.Text);
            await transaction.CommitAsync();
            return CreatedAtAction(nameof(GetById), new { id = comment.Id.ToString() }, comment);
        }
    }
}