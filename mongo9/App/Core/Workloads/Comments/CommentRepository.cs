using LeoMongo;
using LeoMongo.Database;
using LeoMongo.Transaction;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDBDemoApp.Core.Workloads.Posts;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace MongoDBDemoApp.Core.Workloads.Comments;

public sealed class CommentRepository : RepositoryBase<Comment>, ICommentRepository
{
    public CommentRepository(ITransactionProvider transactionProvider, IDatabaseProvider databaseProvider) : base(
        transactionProvider, databaseProvider)
    {
    }

    public override string CollectionName { get; } = MongoUtil.GetCollectionName<Comment>();

    public async Task<IReadOnlyCollection<Comment>> GetCommentsForPost(ObjectId postId)
    {
        // TODO
        return await Query().Where(p=>p.Id == postId).ToListAsync();
    }

    public async Task<IReadOnlyCollection<Comment>> GetUnapprovedComments()
    {
        // TODO
        return await Query().Where(p=>p.Approved == false).ToListAsync();
    }

    public async Task<bool> ApproveComment(ObjectId id)
    {
        var res = await UpdateOneAsync(id, UpdateDefBuilder.Set(c => c.Approved, true));
        return res is { IsAcknowledged: true, ModifiedCount: 1 };
    }
    public async Task<bool> DeleteComment(ObjectId id)
    {
        // TODO
        Comment deletecomment = await Query().FirstAsync(p => p.Id == id);

        if(deletecomment == null)
        {
            return false;
        }
        else
        {
            await DeleteOneAsync(id);
            return true;
        }
    }

    public async Task<Comment> AddComment(Comment comment)
    {
        // TODO
        return await InsertOneAsync(comment);
    }

    public async Task<Comment?> GetCommentById(ObjectId id)
    {
        // TODO
        Comment comment = await Query().FirstAsync(p => p.Id == id);
        if (comment == null)
        {
            return null;
        }
        else
        {
            return comment;
        }
    }

    public async Task DeleteCommentsByPost(ObjectId postId)
    {
        // TODO
        List<Comment?> comments = (List<Comment?>)await GetCommentsForPost(postId);

        foreach (Comment? com in comments)
        {
            await DeleteOneAsync(com!.Id);
        }
    }
}