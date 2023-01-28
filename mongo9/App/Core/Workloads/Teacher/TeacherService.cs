using MongoDB.Bson;
using MongoDBDemoApp.Core.Util;

namespace MongoDBDemoApp.Core.Workloads.Teacher;

public class TeacherService : ITeacherService
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ILogger<TeacherService> _logger;
    private readonly ITeacherRepository _repository;

    public TeacherService(IDateTimeProvider dateTimeProvider, ITeacherRepository repository,
        ILogger<TeacherService> logger)
    {
        _dateTimeProvider = dateTimeProvider;
        _repository = repository;
        _logger = logger;
    }
        
    public Task<IReadOnlyCollection<Teacher>> GetAllTeacher() => _repository.GetAllTeachers();

    public Task<Teacher?> GetTeacherById(ObjectId id) => _repository.GetTeacherById(id);
        
    public Task<Teacher> AddTeacher(string firstName, string lastName)
    {
        var teacher = new Teacher
        {
            FirstName = firstName,
            LastName = lastName
        };
        return _repository.AddTeacher(teacher);
    }

    public async Task DeleteTeacher(ObjectId id)
    {
        await _repository.DeleteTeacher(id);
    }
}