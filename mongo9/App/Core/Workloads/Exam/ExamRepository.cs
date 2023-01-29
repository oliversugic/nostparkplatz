using LeoMongo;
using LeoMongo.Database;
using LeoMongo.Transaction;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;


namespace MongoDBDemoApp.Core.Workloads.Exam;

public sealed class ExamRepository : RepositoryBase<Exam>, IExamRepository
{
    
    public ExamRepository(ITransactionProvider transactionProvider,
        IDatabaseProvider databaseProvider) : base(transactionProvider, databaseProvider)
    {
    }

    public override string CollectionName { get; } =  MongoUtil.GetCollectionName<Exam>();
    public Task<Exam> AddExam(Exam exam)
    {
        return this.InsertOneAsync(exam);
    }

    public async Task<Exam?> GetExamById(ObjectId id)
    {
        Exam exam = await Query().Where(g => g.Id == id).FirstAsync();
        return exam;
    }

    public async Task<IReadOnlyCollection<Exam>> GetAllExam()
    {
        return await Query().ToListAsync();
    }

    public Task DeleteExam(ObjectId examId)
    {
        return Query().ToListAsync();
    }

}