using LeoMongo;
using LeoMongo.Database;
using LeoMongo.Transaction;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

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
            return this.DeleteOneAsync(postId);
        }

        public async Task<Student> Update(Student student)
        {
            await this.UpdateOneAsync(student.Id, 
                Builders<Student>.Update.Set(p => p.FirstName, student.FirstName));
            await this.UpdateOneAsync(student.Id,
                Builders<Student>.Update.Set(p => p.LastName, student.LastName));
            Student updated = await Query().Where(g => g.Id == student.Id).FirstAsync();
            return updated;
        }

        public async Task<IReadOnlyCollection<Student>> GetAllStudents()
        {
            return await Query().ToListAsync();
        }

        public async Task<Student?> GetStudentById(ObjectId id)
        {
            var c = await Query().Where(g => g.Id == id).FirstAsync();
            return c;
        }
    }
}
