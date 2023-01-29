using MongoDB.Bson;
using MongoDBDemoApp.Core.Util;

namespace MongoDBDemoApp.Core.Workloads.Student
{
    public class StudentService: IStudentService
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly ILogger<StudentService> _logger;
        private readonly IStudentRepository _repository;

        public StudentService(IDateTimeProvider dateTimeProvider, IStudentRepository repository,
            ILogger<StudentService> logger)
        {
            _dateTimeProvider = dateTimeProvider;
            _repository = repository;
            _logger = logger;
        }
        
        public Task<IReadOnlyCollection<Student>> GetAllStudents() => _repository.GetAllStudents();

        public Task<Student?> GetStudentById(ObjectId id) => _repository.GetStudentById(id);
        
        public Task<Student> AddStudent(string firstName, string lastName)
        {
            var student = new Student
            {
                FirstName = firstName,
                LastName = lastName
            };
            return _repository.AddStudent(student);
        }

        public async Task DeleteStudent(ObjectId id)
        {
            await _repository.DeleteStudent(id);
        }

        public async Task<Student> Update(Student student)
        {
            return await _repository.Update(student);
        }
    }
}
