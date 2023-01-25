using LeoMongo.Database;
using MongoDB.Bson;

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
