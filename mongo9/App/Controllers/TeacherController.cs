using AutoMapper;
using LeoMongo.Transaction;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDBDemoApp.Core.Util;
using MongoDBDemoApp.Core.Workloads.Student;
using MongoDBDemoApp.Core.Workloads.Teacher;
using MongoDBDemoApp.Model.Student;
using MongoDBDemoApp.Model.Teacher;

namespace MongoDBDemoApp.Controllers;


[Route("api/[controller]")]
[ApiController]
public sealed class TeacherController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ITeacherService _service;
    private readonly ITransactionProvider _transactionProvider;

    public TeacherController(IMapper mapper, ITeacherService service, ITransactionProvider transactionProvider)
    {
        _mapper = mapper;
        _service = service;
        _transactionProvider = transactionProvider;
    }

    /// <summary>
    ///     Removes the teacher identified by the given id.
    ///     If the id does not exist nothing will be changed.
    /// </summary>
    /// <param name="id">if of an existing teacher</param>
    [HttpDelete]
    public async Task<IActionResult> Delete(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            return BadRequest();
        }

        using var transaction = await _transactionProvider.BeginTransaction();
        await _service.DeleteTeacher(new(id));
        await transaction.CommitAsync();
        return Ok();
    }

    /// <summary>
    ///     Returns all teacher.
    /// </summary>
    /// <returns>All existing teacher</returns>
    [HttpGet]
    [Route("all")]
    public async Task<ActionResult<IReadOnlyCollection<TeacherDTO>>> GetAll()
    {
        const int TeaserLength = 250;

        IReadOnlyCollection<Teacher> teacher = await _service.GetAllTeacher();
        return Ok(_mapper.Map<List<TeacherDTO>>(teacher,
            options =>
            {
                options.AfterMap((_, pl) =>
                    pl.ForEach(p => p.FirstName = p.FirstName.Truncate(TeaserLength)));
            }));
    }
    
    /// <summary>
    ///     Returns the teacher identified by the given id if it exists.
    /// </summary>
    /// <param name="id">existing teacher id</param>
    /// <returns>a teacher</returns>
    [HttpGet]
    public async Task<ActionResult<TeacherDTO>> GetById(string id)
    {
        Teacher? teacher;
        if (string.IsNullOrWhiteSpace(id)
            || (teacher = await _service.GetTeacherById(new ObjectId(id))) == null)
        {
            return BadRequest();
        }

        return Ok(_mapper.Map<TeacherDTO>(teacher));
    }

    /// <summary>
    ///     Creates a new teacher.
    /// </summary>
    /// <param name="request">Data for the new teacher</param>
    /// <returns>the created teacher if successful</returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTeacherRequest request)
    {
        // TODO
        if (string.IsNullOrWhiteSpace(request.FirstName)
            || string.IsNullOrWhiteSpace(request.LastName))
        {
            return BadRequest();
        }

        using var transaction = await _transactionProvider.BeginTransaction();
        Teacher teacher = await _service.AddTeacher(request.FirstName, request.LastName);
        await transaction.CommitAsync();
        return CreatedAtAction(nameof(GetById), new { id = teacher.Id.ToString() }, teacher);
    }
    
    /// <summary>
    ///     Returns the updated object
    /// </summary>
    /// <param name="id">Object with updates</param>
    /// <returns>a teacher</returns>
    [HttpPut]
    public async Task<ActionResult<TeacherDTO>> Update([FromBody] TeacherDTO request)
    {
        Teacher? teacher;
        if (string.IsNullOrWhiteSpace(request.FirstName)
            || string.IsNullOrWhiteSpace(request.LastName)
            || (teacher = await _service.GetTeacherById(new ObjectId(request.Id))) == null)
        {
            return BadRequest();
        }
        
        using var transaction = await _transactionProvider.BeginTransaction();
        teacher.FirstName = request.FirstName;
        teacher.LastName = request.LastName;
        Teacher updated = await _service.Update(teacher);
        await transaction.CommitAsync();
        return Ok(_mapper.Map<TeacherDTO>(updated));
    }
}