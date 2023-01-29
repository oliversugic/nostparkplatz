using MongoDB.Bson;
using MongoDBDemoApp.Core.Util;
using MongoDBDemoApp.Core.Workloads.Student;
using MongoDBDemoApp.Core.Workloads.Subject;
using MongoDBDemoApp.Core.Workloads.Teacher;
using ZstdSharp.Unsafe;

namespace MongoDBDemoApp.Core.Workloads.Exam;

public class ExamService : IExamService
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ILogger<ExamService> _logger;
    private readonly IExamRepository _repository;
    private readonly IStudentRepository _stuRep;
    private readonly ITeacherRepository _teachRep;
    private readonly ISubjectRepository _subRep;

    public ExamService(IDateTimeProvider dateTimeProvider, IExamRepository repository,
        ISubjectRepository subRep, ITeacherRepository teachRep,
        IStudentRepository stuRep,
        ILogger<ExamService> logger)
    {
        _dateTimeProvider = dateTimeProvider;
        _repository = repository;
        _logger = logger;
        _stuRep = stuRep;
        _teachRep = teachRep;
        _subRep = subRep;
    }
    
    public Task<IReadOnlyCollection<Exam>> GetAllExams()=> _repository.GetAllExam();
    public Task<Exam?> GetExamById(ObjectId objectId) => _repository.GetExamById(objectId);

    public async Task<Exam> AddExam(bool requestPassedExam, int requestAttempt, DateTime requestDate, int requestGrade,
        string requestStudentId, string requestTeacherId, string requestSubjectId)
    {
        Student.Student? student = await _stuRep.GetStudentById(ObjectId.Parse(requestStudentId));
        Teacher.Teacher? teacher = await _teachRep.GetTeacherById(ObjectId.Parse(requestTeacherId));
        Subject.Subject? subject = await _subRep.GetSubjectById(ObjectId.Parse(requestSubjectId));
        Exam exam = new Exam()
        {
            StudentId = ObjectId.Parse(requestStudentId),
            SubjectId = ObjectId.Parse(requestSubjectId),
            TeacherId= ObjectId.Parse(requestTeacherId),
            Attempt = requestAttempt,
            Date = requestDate,
            PassedExam = requestPassedExam,
            Grade = requestGrade
        };
        return await _repository.AddExam(exam);
    }
}