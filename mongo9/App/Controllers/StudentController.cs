using AutoMapper;
using LeoMongo.Transaction;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDBDemoApp.Core.Util;
using MongoDBDemoApp.Core.Workloads.Student;
using MongoDBDemoApp.Model.Student;

namespace MongoDBDemoApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class StudentController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IStudentService _service;
    private readonly ITransactionProvider _transactionProvider;

    public StudentController(IMapper mapper, IStudentService service, ITransactionProvider transactionProvider)
    {
        _mapper = mapper;
        _service = service;
        _transactionProvider = transactionProvider;
    }

    /// <summary>
    ///     Removes the student identified by the given id.
    ///     If the id does not exist nothing will be changed.
    /// </summary>
    /// <param name="id">if of an existing student</param>
    [HttpDelete]
    public async Task<IActionResult> Delete(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            return BadRequest();
        }

        using var transaction = await _transactionProvider.BeginTransaction();
        await _service.DeleteStudent(new(id));
        await transaction.CommitAsync();
        return Ok();
    }

    /// <summary>
    ///     Returns all students.
    /// </summary>
    /// <returns>All existing students</returns>
    [HttpGet]
    [Route("all")]
    public async Task<ActionResult<IReadOnlyCollection<StudentDTO>>> GetAll()
    {
        const int TeaserLength = 250;

        IReadOnlyCollection<Student> students = await _service.GetAllStudents();
        return Ok(_mapper.Map<List<StudentDTO>>(students,
            options =>
            {
                options.AfterMap((_, pl) =>
                    pl.ForEach(p => p.FirstName = p.FirstName.Truncate(TeaserLength)));
            }));
    }
    
    /// <summary>
    ///     Returns the student identified by the given id if it exists.
    /// </summary>
    /// <param name="id">existing student id</param>
    /// <returns>a student</returns>
    [HttpGet]
    public async Task<ActionResult<StudentDTO>> GetById(string id)
    {
        Student? student;
        if (string.IsNullOrWhiteSpace(id)
            || (student = await _service.GetStudentById(new ObjectId(id))) == null)
        {
            return BadRequest();
        }

        return Ok(_mapper.Map<StudentDTO>(student));
    }

    /// <summary>
    ///     Creates a new Student.
    /// </summary>
    /// <param name="request">Data for the new student</param>
    /// <returns>the created student if successful</returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStudentRequest request)
    {
        // TODO
        if (string.IsNullOrWhiteSpace(request.FirstName)
            || string.IsNullOrWhiteSpace(request.LastName))
        {
            return BadRequest();
        }

        using var transaction = await _transactionProvider.BeginTransaction();
        Student student = await _service.AddStudent(request.FirstName, request.LastName);
        await transaction.CommitAsync();
        return CreatedAtAction(nameof(GetById), new { id = student.Id.ToString() }, student);
    }
    
    /// <summary>
    ///     Returns the updated object
    /// </summary>
    /// <param name="id">Object with updates</param>
    /// <returns>a student</returns>
    [HttpPut]
    public async Task<ActionResult<StudentDTO>> Update([FromBody] StudentDTO request)
    {
        Student? student;
        if (string.IsNullOrWhiteSpace(request.FirstName)
            || string.IsNullOrWhiteSpace(request.LastName)
            || (student = await _service.GetStudentById(new ObjectId(request.Id))) == null)
        {
            return BadRequest();
        }
        
        using var transaction = await _transactionProvider.BeginTransaction();
        student.FirstName = request.FirstName;
        student.LastName = request.LastName;
        Student updated = await _service.Update(student);
        await transaction.CommitAsync();
        
        return Ok(_mapper.Map<StudentDTO>(updated));
    }
}