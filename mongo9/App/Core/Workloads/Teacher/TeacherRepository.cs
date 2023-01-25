using LeoMongo;
using LeoMongo.Database;
using LeoMongo.Transaction;
using MongoDB.Bson;

namespace MongoDBDemoApp.Core.Workloads.Teacher;

public sealed class TeacherRepository: RepositoryBase<Teacher>, ITeacherRepository
{
    public override string CollectionName { get; } = MongoUtil.GetCollectionName<Teacher>();
    
    public Task<Teacher> AddStudent(Teacher comment)
    {
        throw new NotImplementedException();
    }

    public Task<Teacher?> GetStudentById(ObjectId id)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<Teacher>> GetAllStudents()
    {
        throw new NotImplementedException();
    }

    public Task DeleteStudent(ObjectId postId)
    {
        throw new NotImplementedException();
    }

    public TeacherRepository(ITransactionProvider transactionProvider,
        IDatabaseProvider databaseProvider) : base(transactionProvider, databaseProvider)
    {
    }
}