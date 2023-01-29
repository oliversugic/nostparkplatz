using LeoMongo.Database;
using MongoDB.Bson;

namespace MongoDBDemoApp.Core.Workloads.Competence;

public interface ICompetenceRepository : IRepositoryBase
{
    Task<Competence> AddCompetence(Competence comment);
    Task<Competence?> GetCompetenceById(ObjectId id);
    Task<IReadOnlyCollection<Competence>> GetAllCompetences();
    Task DeleteCompetence(ObjectId competenceId);
    Task<Competence> Update(Competence competence);
    Task<IReadOnlyCollection<Competence>?> GetCompetencesForSubject(ObjectId subjectId);
    Task DeleteCompetencesBySubject(ObjectId id);
}