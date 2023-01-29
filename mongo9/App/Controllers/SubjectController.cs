using AutoMapper;
using LeoMongo.Transaction;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDBDemoApp.Core.Util;
using MongoDBDemoApp.Core.Workloads.Subject;
using MongoDBDemoApp.Model.Subject;

namespace MongoDBDemoApp.Controllers;


[Route("api/[controller]")]
[ApiController]
public sealed class SubjectController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ISubjectService _service;
    private readonly ITransactionProvider _transactionProvider;

    public SubjectController(IMapper mapper, ISubjectService service, ITransactionProvider transactionProvider)
    {
        _mapper = mapper;
        _service = service;
        _transactionProvider = transactionProvider;
    }
    
    /// <summary>
    ///     Returns all subjects.
    /// </summary>
    /// <returns>All existing subjects</returns>
    [HttpGet]
    [Route("all")]
    public async Task<ActionResult<IReadOnlyCollection<SubjectDTO>>> GetAll()
    {
        const int TeaserLength = 250;

        IReadOnlyCollection<Subject> subjects = await _service.GetAllSubjects();
        return Ok(_mapper.Map<List<SubjectDTO>>(subjects,
            options =>
            {
                options.AfterMap((_, pl) =>
                    pl.ForEach(p => p.Name = p.Name.Truncate(TeaserLength)));
            }));
    }

    /// <summary>
    ///     Returns the subject identified by the given id if it exists.
    /// </summary>
    /// <param name="id">existing subject id</param>
    /// <returns>a subject</returns>
    [HttpGet]
    public async Task<ActionResult<SubjectDTO>> GetById(string id)
    {
        Subject? subject;
        if (string.IsNullOrWhiteSpace(id)
            || (subject = await _service.GetSubjectById(new ObjectId(id))) == null)
        {
            return BadRequest();
        }
        return Ok(_mapper.Map<SubjectDTO>(subject));
    }
    
    /// <summary>
    ///     Creates a new subject.
    /// </summary>
    /// <param name="request">Data for the new subject</param>
    /// <returns>the created subject if successful</returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSubjectRequest request)
    {
        // TODO
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return BadRequest();
        }

        using var transaction = await _transactionProvider.BeginTransaction();
        Subject subject = await _service.AddSubject(request.Name);
        await transaction.CommitAsync();
        return CreatedAtAction(nameof(GetById), new { id = subject.Id.ToString() }, subject);
    }

    /// <summary>
    ///     Removes the subject identified by the given id.
    ///     If the id does not exist nothing will be changed.
    /// </summary>
    /// <param name="id">if of an existing subject</param>
    [HttpDelete]
    public async Task<IActionResult> Delete(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            return BadRequest();
        }

        using var transaction = await _transactionProvider.BeginTransaction();
        await _service.DeleteSubject(id);
        await transaction.CommitAsync();
        return Ok();
    }
    
    /// <summary>
    ///     Returns the updated object
    /// </summary>
    /// <param name="id">Object with updates</param>
    /// <returns>a subject</returns>
    [HttpPut]
    public async Task<ActionResult<SubjectDTO>> Update([FromBody] SubjectDTO request)
    {
        Subject? subject;
        if (string.IsNullOrWhiteSpace(request.Name)
            || (subject = await _service.GetSubjectById(new ObjectId(request.Id))) == null)
        {
            return BadRequest();
        }
        
        using var transaction = await _transactionProvider.BeginTransaction();
        subject.Name = request.Name;
        Subject updated = await _service.Update(subject);
        await transaction.CommitAsync();
        
        return Ok(_mapper.Map<SubjectDTO>(updated));
    }
}