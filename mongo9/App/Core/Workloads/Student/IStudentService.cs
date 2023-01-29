using MongoDB.Bson;

namespace MongoDBDemoApp.Core.Workloads.Student
{
    public interface IStudentService
    {
        Task<IReadOnlyCollection<Student>> GetAllStudents();
        Task<Student?> GetStudentById(ObjectId id);
        Task<Student> AddStudent(string firstName, string lastName);
        Task DeleteStudent(ObjectId id);
        Task<Student> Update(Student student);
    }
}
