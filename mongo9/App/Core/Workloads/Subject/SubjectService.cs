using MongoDB.Bson;
using MongoDBDemoApp.Core.Util;
using MongoDBDemoApp.Core.Workloads.Competence;
using MongoDBDemoApp.Core.Workloads.Student;

namespace MongoDBDemoApp.Core.Workloads.Subject;

public class SubjectService:ISubjectService
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ILogger<SubjectService> _logger;
    private readonly ISubjectRepository _repository;
    private readonly ICompetenceRepository _comrepo;

    public SubjectService(IDateTimeProvider dateTimeProvider, ISubjectRepository repository,
        ICompetenceRepository comrepo,
        ILogger<SubjectService> logger)
    {
        _dateTimeProvider = dateTimeProvider;
        _repository = repository;
        _logger = logger;
        _comrepo = comrepo;
    }
    public Task<IReadOnlyCollection<Subject>> GetAllSubjects() => _repository.GetAllSubject();

    public Task<Subject?> GetSubjectById(ObjectId id) => _repository.GetSubjectById(id);
    public Task<Subject> AddSubject(string requestName)
    {
        Subject subject = new Subject()
        {
            Name = requestName
        };
        return _repository.AddSubject(subject);
    }

    public async Task DeleteSubject(string stringid)
    {
        ObjectId id = ObjectId.Parse(stringid);
        (ObjectId SubjectId, List<ObjectId>? CompetenceIds)? subjectWithCompetences =
            await _repository.GetCompetencesWithSubject(id);
        if (subjectWithCompetences == null)
        {
            throw new ArgumentException(nameof(id));
        }
        List<ObjectId>? competences = subjectWithCompetences.Value.CompetenceIds;
        if (competences != null && competences.Count > 0)
        {
            _logger.LogInformation($"Deleting {competences.Count} comment(s) together with post {id}.");
            await _comrepo.DeleteCompetencesBySubject(id);
        }
        await _repository.DeleteSubject(subjectWithCompetences.Value.SubjectId);
    }

    public async Task<Subject> Update(Subject subject)
    {
        return await _repository.Update(subject);
    }
}