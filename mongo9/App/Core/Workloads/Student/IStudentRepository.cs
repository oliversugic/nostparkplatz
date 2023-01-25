using LeoMongo.Database;
using MongoDB.Bson;
using MongoDBDemoApp.Core.Workloads.Comments;

namespace MongoDBDemoApp.Core.Workloads.Student
{
    public interface IStudentRepository : IRepositoryBase
    {
        Task<Student> AddStudent(Student comment);
        Task<Student?> GetStudentById(ObjectId id);
        Task<IReadOnlyCollection<Student>> GetAllStudents();
        Task DeleteStudent(ObjectId postId);
    }
}
