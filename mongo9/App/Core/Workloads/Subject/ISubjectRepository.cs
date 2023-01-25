using MongoDB.Bson;

namespace MongoDBDemoApp.Core.Workloads.Subject;

public interface ISubjectRepository
{
    Task<Subject> AddSubject(Subject comment);
    Task<Subject?> GetSubjectById(ObjectId id);
    Task<IReadOnlyCollection<Subject>> GetAllSubject();
    Task DeleteSubject(ObjectId postId);
}