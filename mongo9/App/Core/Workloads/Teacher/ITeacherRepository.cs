using LeoMongo.Database;
using MongoDB.Bson;

namespace MongoDBDemoApp.Core.Workloads.Teacher;

public interface ITeacherRepository: IRepositoryBase
{
    Task<Teacher> AddStudent(Teacher comment);
    Task<Teacher?> GetStudentById(ObjectId id);
    Task<IReadOnlyCollection<Teacher>> GetAllStudents();
    Task DeleteStudent(ObjectId postId);
}