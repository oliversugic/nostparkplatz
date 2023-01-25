using MongoDB.Bson;

namespace MongoDBDemoApp.Core.Workloads.Competence;

public interface ICompetenceRepository
{
    Task<Competence> AddCompetence(Competence comment);
    Task<Competence?> GetCompetenceById(ObjectId id);
    Task<IReadOnlyCollection<Competence>> GetAllCompetences();
    Task DeleteCompetence(ObjectId competenceId);
}