using LeoMongo;
using LeoMongo.Database;
using LeoMongo.Transaction;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace MongoDBDemoApp.Core.Workloads.Subject;

public class SubjectRepository: RepositoryBase<Subject>,ISubjectRepository
{
    
    public SubjectRepository(ITransactionProvider transactionProvider,
        IDatabaseProvider databaseProvider) : base(transactionProvider, databaseProvider)
    {
    }

    public override string CollectionName { get; } = MongoUtil.GetCollectionName<Subject>();
    
    public async Task<Subject> AddSubject(Subject comment)
    {
        return await this.InsertOneAsync(comment);
    }

    public async Task<Subject?> GetSubjectById(ObjectId id)
    {
        var c = await Query().Where(g => g.Id == id).FirstAsync();
        return c;
    }

    public async Task<IReadOnlyCollection<Subject>> GetAllSubject()
    {
        return await Query().ToListAsync();
    }

    public Task DeleteSubject(ObjectId postId)
    {
        throw new NotImplementedException();
    }

}