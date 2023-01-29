using MongoDB.Bson;

namespace MongoDBDemoApp.Core.Workloads.Subject;

public interface ISubjectService
{
    Task<IReadOnlyCollection<Subject>> GetAllSubjects();
    Task<Subject?> GetSubjectById(ObjectId id);
    Task<Subject> AddSubject(string requestName);
}