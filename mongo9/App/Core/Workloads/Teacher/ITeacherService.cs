using MongoDB.Bson;

namespace MongoDBDemoApp.Core.Workloads.Teacher;

public interface ITeacherService
{
    Task<IReadOnlyCollection<Teacher>> GetAllTeacher();
    Task<Teacher?> GetTeacherById(ObjectId id);
    Task<Teacher> AddTeacher(string firstName, string lastName);
    Task DeleteTeacher(ObjectId id);
}