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
    public Task<Competence> AddCompetence(string name, string description)
    {
        var competence = new Competence()
        {
            Compentences = name,
            Descripton = description
        };
        return _repository.AddCompetence(competence);
    }

    public async Task DeleteCompetence(ObjectId id)
    {
        await _repository.DeleteCompetence(id);
    }
}