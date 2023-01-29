using AutoMapper;
using LeoMongo.Transaction;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDBDemoApp.Core.Workloads.Competence;
using MongoDBDemoApp.Core.Workloads.Exam;
using MongoDBDemoApp.Model.Exam;

namespace MongoDBDemoApp.Controllers;


[Route("api/[controller]")]
[ApiController]
public sealed class ExamController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IExamService _service;
    private readonly ITransactionProvider _transactionProvider;

    public ExamController(IMapper mapper, IExamService service, ITransactionProvider transactionProvider)
    {
        _mapper = mapper;
        _service = service;
        _transactionProvider = transactionProvider;
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
        if (string.IsNullOrWhiteSpace(request.SubjectId)
            || string.IsNullOrWhiteSpace(request.Attempt.ToString())
            || string.IsNullOrWhiteSpace(request.Date.ToString())
            || string.IsNullOrWhiteSpace(request.Grade.ToString())
            || string.IsNullOrWhiteSpace(request.StudentId)
            || string.IsNullOrWhiteSpace(request.TeacherId)
            || string.IsNullOrWhiteSpace(request.PassedExam.ToString()))
        {
            return BadRequest();
        }
        using var transaction = await _transactionProvider.BeginTransaction();
        Exam exam = await _service.AddExam(request.PassedExam, request.Attempt,request.Date, request.Grade,
           request.StudentId, request.TeacherId, request.SubjectId);
        await transaction.CommitAsync();   
        return CreatedAtAction(nameof(GetById), new { id = exam.Id.ToString() }, exam);
    }
}