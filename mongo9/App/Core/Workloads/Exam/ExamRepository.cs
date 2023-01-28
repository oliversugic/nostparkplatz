using LeoMongo.Database;
using LeoMongo.Transaction;
using MongoDB.Bson;

namespace MongoDBDemoApp.Core.Workloads.Exam;

public sealed class ExamRepository : RepositoryBase<Exam>, IExamRepository
{
    
    public ExamRepository(ITransactionProvider transactionProvider,
        IDatabaseProvider databaseProvider) : base(transactionProvider, databaseProvider)
    {
    }

    public override string CollectionName { get; } = default!;
    public Task<Exam> AddExam(Exam comment)
    {
        throw new NotImplementedException();
    }

    public Task<Exam?> GetExamById(ObjectId id)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<Exam>> GetAllExam()
    {
        throw new NotImplementedException();
    }

    public Task DeleteExam(ObjectId examId)
    {
        throw new NotImplementedException();
    }

}