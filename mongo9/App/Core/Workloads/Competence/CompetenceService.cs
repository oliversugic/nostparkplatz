using MongoDB.Bson;
using MongoDBDemoApp.Core.Util;

namespace MongoDBDemoApp.Core.Workloads.Competence;

public class CompetenceService: ICompetenceService
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ILogger<CompetenceService> _logger;
    private readonly ICompetenceRepository _repository;

    public CompetenceService(IDateTimeProvider dateTimeProvider, ICompetenceRepository repository,
        ILogger<CompetenceService> logger)
    {
        _dateTimeProvider = dateTimeProvider;
        _repository = repository;
        _logger = logger;
    }
    
    public Task<IReadOnlyCollection<Competence>> GetAllCompetences() => _repository.GetAllCompetences();

    public Task<Competence?> GetCompetenceById(ObjectId id) => _repository.GetCompetenceById(id);
    public Task<Competence> AddCompetence(string id, string name, string description)
    {
        Subject.Subject s = new Subject.Subject();
        s.Id = ObjectId.Parse(id);
        var competence = new Competence();
        competence.Compentences = name;
        competence.Descripton = description;
        competence.SubjectId = s.Id;
        return _repository.AddCompetence(competence);
    }

    public async Task DeleteCompetence(ObjectId id)
    {
        await _repository.DeleteCompetence(id);
    }

    public async Task<Competence> Update(Competence competence)
    {
        return await _repository.Update(competence);
    }
}