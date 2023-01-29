using LeoMongo.Database;
using MongoDB.Bson;

namespace MongoDBDemoApp.Core.Workloads.Teacher;

public interface ITeacherRepository: IRepositoryBase
{
    Task<Teacher> AddTeacher(Teacher teacher);
    Task<Teacher?> GetTeacherById(ObjectId id);
    Task<IReadOnlyCollection<Teacher>> GetAllTeachers();
    Task DeleteTeacher(ObjectId teacherId);
    Task<Teacher> Update(Teacher teacher);
}