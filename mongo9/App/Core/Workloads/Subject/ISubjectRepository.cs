using LeoMongo.Database;
using MongoDB.Bson;

namespace MongoDBDemoApp.Core.Workloads.Subject;

public interface ISubjectRepository : IRepositoryBase
{
    Task<Subject> AddSubject(Subject comment);
    Task<Subject?> GetSubjectById(ObjectId id);
    Task<IReadOnlyCollection<Subject>> GetAllSubject();
    Task DeleteSubject(ObjectId postId);
    Task<(ObjectId SubjectId, List<ObjectId>? CompetenceIds)?> GetCompetencesWithSubject(ObjectId id);
    Task<Subject> Update(Subject subject);
}