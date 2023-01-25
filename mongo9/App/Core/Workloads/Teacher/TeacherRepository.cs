using LeoMongo;
using LeoMongo.Database;
using LeoMongo.Transaction;
using MongoDB.Bson;

namespace MongoDBDemoApp.Core.Workloads.Teacher;

public sealed class TeacherRepository: RepositoryBase<Teacher>, ITeacherRepository
{
    public TeacherRepository(ITransactionProvider transactionProvider, IDatabaseProvider databaseProvider) : base(transactionProvider, databaseProvider)
    {
    }

    public override string CollectionName { get; } = default!;
    public Task<Teacher> AddTeacher(Teacher teacher)
    {
        throw new NotImplementedException();
    }

    public Task<Teacher?> GetTeacherById(ObjectId id)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<Teacher>> GetAllTeachers()
    {
        throw new NotImplementedException();
    }

    public Task DeleteTeacher(ObjectId teacherId)
    {
        throw new NotImplementedException();
    }
}