using LeoMongo.Database;
using LeoMongo.Transaction;
using MongoDB.Bson;

namespace MongoDBDemoApp.Core.Workloads.Subject;

public class SubjectRepository: RepositoryBase<Subject>,ISubjectRepository
{
    
    public SubjectRepository(ITransactionProvider transactionProvider,
        IDatabaseProvider databaseProvider) : base(transactionProvider, databaseProvider)
    {
    }

    public override string CollectionName { get; } = default!;
    
    public Task<Subject> AddSubject(Subject comment)
    {
        throw new NotImplementedException();
    }

    public Task<Subject?> GetSubjectById(ObjectId id)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<Subject>> GetAllSubject()
    {
        throw new NotImplementedException();
    }

    public Task DeleteSubject(ObjectId postId)
    {
        throw new NotImplementedException();
    }

}