using MongoDB.Bson;
using MongoDBDemoApp.Core.Util;
using MongoDBDemoApp.Core.Workloads.Student;

namespace MongoDBDemoApp.Core.Workloads.Subject;

public class SubjectService:ISubjectService
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ILogger<SubjectService> _logger;
    private readonly ISubjectRepository _repository;

    public SubjectService(IDateTimeProvider dateTimeProvider, ISubjectRepository repository,
        ILogger<SubjectService> logger)
    {
        _dateTimeProvider = dateTimeProvider;
        _repository = repository;
        _logger = logger;
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
}