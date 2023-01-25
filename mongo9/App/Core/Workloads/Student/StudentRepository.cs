using LeoMongo;
using LeoMongo.Database;
using LeoMongo.Transaction;
using MongoDB.Bson;

namespace MongoDBDemoApp.Core.Workloads.Student
{
    public sealed class StudentRepository : RepositoryBase<Student>, IStudentRepository
    {
        public StudentRepository(ITransactionProvider transactionProvider, IDatabaseProvider databaseProvider) : base(
            transactionProvider, databaseProvider)
        {
        }

        public override string CollectionName { get; } = MongoUtil.GetCollectionName<Student>();

        public async Task<Student> AddStudent(Student comment)
        {
            return await InsertOneAsync(comment);
        }

        public Task DeleteStudent(ObjectId postId)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<Student>> GetAllStudents()
        {
            throw new NotImplementedException();
        }

        public Task<Student?> GetStudentById(ObjectId id)
        {
            throw new NotImplementedException();
        }
    }
}
