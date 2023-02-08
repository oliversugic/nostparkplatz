using AutoMapper;
using LeoMongo.Transaction;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDBDemoApp.Core.Workloads.Competence;
using MongoDBDemoApp.Core.Workloads.Exam;
using MongoDBDemoApp.Core.Workloads.Student;
using MongoDBDemoApp.Core.Workloads.Subject;
using MongoDBDemoApp.Core.Workloads.Teacher;
using MongoDBDemoApp.Model.Exam;

namespace MongoDBDemoApp.Controllers;


[Route("api/[controller]")]
[ApiController]
public sealed class ExamController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IExamService _service;
    private readonly ITeacherService _teacherService;
    private readonly IStudentService _studentService;
    private readonly ISubjectService _subjectService;
    private readonly ICompetenceService _competenceService;
    private readonly ITransactionProvider _transactionProvider;

    public ExamController(IMapper mapper, IExamService service,
        ITeacherService teacherService, 
        IStudentService studentService,
        ISubjectService subjectService,
        ICompetenceService competenceService,
        ITransactionProvider transactionProvider)
    {
        _mapper = mapper;
        _service = service;
        _teacherService = teacherService;
        _studentService = studentService;
        _subjectService = subjectService;
        _competenceService = competenceService;
        _transactionProvider = transactionProvider;
    }
    
    /// <summary>
    ///     Sets Test Data.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("setTestData")]
    public async Task<IActionResult> SetTestData()
    {
        IReadOnlyCollection<Teacher> teachers = await _teacherService.GetAllTeacher();
        if (teachers.Count == 0)
        {
            await _teacherService.AddTeacher("Pete", "Bauer");
            await _teacherService.AddTeacher("Markus", "Kerschi");
            teachers = await _teacherService.GetAllTeacher();
            await _studentService.AddStudent("Paul", "Binderix");
            await _studentService.AddStudent("Oliver", "Guguc");
            IReadOnlyCollection<Student> students = await _studentService.GetAllStudents();
            await _subjectService.AddSubject("Mathe");
            await _subjectService.AddSubject("DBI");
            await _subjectService.AddSubject("PROO");
            IReadOnlyCollection<Subject> subjects = await _subjectService.GetAllSubjects();
            await _competenceService.AddCompetence(subjects.First().Id.ToString(), "Matrizen", "drecks teil");
            await _competenceService.AddCompetence(subjects.Last().Id.ToString(), "MongoDB", "nechste nostla");
            await _service.AddExam(false, 1, DateTime.Now, 5,
                students.First().Id.ToString(), teachers.First().Id.ToString(),
                subjects.First().Id.ToString());
            await _service.AddExam(false, 2, DateTime.Now, 5,
                students.First().Id.ToString(), teachers.First().Id.ToString(),
                subjects.First().Id.ToString());   
            await _service.AddExam(true, 3, DateTime.Now, 4,
                students.First().Id.ToString(), teachers.First().Id.ToString(),
                subjects.First().Id.ToString()); 
        }
        return Ok();
    }
    
    /// <summary>
    ///     Returns all exams.
    /// </summary>
    /// <returns>All existing exams</returns>
    [HttpGet]
    [Route("all")]
    public async Task<ActionResult<IReadOnlyCollection<ExamDTO>>> GetAll()
    {
        IReadOnlyCollection<Exam> exams = await _service.GetAllExams();
        return Ok(_mapper.Map<List<ExamDTO>>(exams));
    }
    
    /// <summary>
    ///     Returns the exam identified by the given id if it exists.
    /// </summary>
    /// <param name="id">existing exam id</param>
    /// <returns>a exam</returns>
    [HttpGet]
    public async Task<ActionResult<ExamDTO>> GetById(string id)
    {
        Exam? exam;
        if (string.IsNullOrWhiteSpace(id)
            || (exam = await _service.GetExamById(new ObjectId(id))) == null)
        {
            return BadRequest();
        }

        return Ok(_mapper.Map<ExamDTO>(exam));
    }

    
    
    /// <summary>
    ///     Creates a new exam.
    /// </summary>
    /// <param name="request">Data for the new exam</param>
    /// <returns>the created exam if successful</returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateExamRequest request)
    {
        // TODO
        if (string.IsNullOrWhiteSpace(request.studentId)
            || string.IsNullOrWhiteSpace(request.teacherId)
            || string.IsNullOrWhiteSpace(request.subjectId)
            || string.IsNullOrWhiteSpace(request.date.ToString())
            || string.IsNullOrWhiteSpace(request.attempt.ToString())
            )
        {
            return BadRequest();
        }
        using var transaction = await _transactionProvider.BeginTransaction();
        Exam exam = await _service.AddExam(false, request.attempt, request.date, 5,
           request.studentId, request.teacherId, request.subjectId);
        await transaction.CommitAsync();   
        return CreatedAtAction(nameof(GetById), new { id = exam.Id.ToString() }, exam);
    }
}