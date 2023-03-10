using MongoDB.Bson;

namespace MongoDBDemoApp.Core.Workloads.Competence;

public interface ICompetenceService
{
    Task<IReadOnlyCollection<Competence>> GetAllCompetences();
    Task<Competence?> GetCompetenceById(ObjectId id);
    Task<Competence> AddCompetence(string id, string name, string description);
    Task DeleteCompetence(ObjectId id);
    Task<Competence> Update(Competence competence);
    Task<IReadOnlyCollection<Competence>?> GetCompetencesForSubject(Subject.Subject subject);
}