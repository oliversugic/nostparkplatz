using AutoMapper;
using LeoMongo.Transaction;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDBDemoApp.Core.Util;
using MongoDBDemoApp.Core.Workloads.Competence;
using MongoDBDemoApp.Core.Workloads.Student;
using MongoDBDemoApp.Core.Workloads.Subject;
using MongoDBDemoApp.Model.Competence;
using MongoDBDemoApp.Model.Student;

namespace MongoDBDemoApp.Controllers;


[Route("api/[controller]")]
[ApiController]
public sealed class CompetenceController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICompetenceService _service;
    private readonly ISubjectService _subService;
    private readonly ITransactionProvider _transactionProvider;

    public CompetenceController(IMapper mapper, ICompetenceService service, 
        ISubjectService subServ, ITransactionProvider transactionProvider)
    {
        _subService = subServ;
        _mapper = mapper;
        _service = service;
        _transactionProvider = transactionProvider;
    }

    /// <summary>
    ///     Removes the competence identified by the given id.
    ///     If the id does not exist nothing will be changed.
    /// </summary>
    /// <param name="id">if of an existing competence</param>
    [HttpDelete]
    public async Task<IActionResult> Delete(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            return BadRequest();
        }

        using var transaction = await _transactionProvider.BeginTransaction();
        await _service.DeleteCompetence(new(id));
        await transaction.CommitAsync();
        return Ok();
    }

    /// <summary>
    ///     Returns all competences.
    /// </summary>
    /// <returns>All existing competences</returns>
    [HttpGet]
    [Route("all")]
    public async Task<ActionResult<IReadOnlyCollection<CompetenceDTO>>> GetAll()
    {
        const int TeaserLength = 250;

        IReadOnlyCollection<Competence> competences = await _service.GetAllCompetences();
        return Ok(_mapper.Map<List<CompetenceDTO>>(competences,
            options =>
            {
                options.AfterMap((_, pl) =>
                    pl.ForEach(p => p.Descripton = p.Descripton.Truncate(TeaserLength)));
            }));
    }
    
    /// <summary>
    ///     Returns the competence identified by the given id if it exists.
    /// </summary>
    /// <param name="id">existing competence id</param>
    /// <returns>a competence</returns>
    [HttpGet]
    public async Task<ActionResult<CompetenceDTO>> GetById(string id)
    {
        Competence? competence;
        if (string.IsNullOrWhiteSpace(id)
            || (competence = await _service.GetCompetenceById(new ObjectId(id))) == null)
        {
            return BadRequest();
        }

        return Ok(_mapper.Map<CompetenceDTO>(competence));
    }

    /// <summary>
    ///     Creates a new competence.
    /// </summary>
    /// <param name="request">Data for the new competence</param>
    /// <returns>the created competence if successful</returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCompetenceRequest request)
    {
        // TODO
        if (string.IsNullOrWhiteSpace(request.Compentences)
            || string.IsNullOrWhiteSpace(request.Descripton)
            || string.IsNullOrWhiteSpace(request.SubjectId))
        {
            return BadRequest();
        }
        using var transaction = await _transactionProvider.BeginTransaction();
        Competence competence = await _service.AddCompetence(request.SubjectId, request.Compentences, request.Descripton);
        await transaction.CommitAsync();   
        return CreatedAtAction(nameof(GetById), new { id = competence.Id.ToString() }, competence);
    }
    
    
    /// <summary>
    ///     Returns the updated object
    /// </summary>
    /// <param name="id">Object with updates</param>
    /// <returns>a competence</returns>
    [HttpPut]
    public async Task<ActionResult<CompetenceDTO>> Update([FromBody] CompetenceDTO request)
    {
        Competence? competence;
        if (string.IsNullOrWhiteSpace(request.Compentences)
            || string.IsNullOrWhiteSpace(request.Descripton)
            || (competence = await _service.GetCompetenceById(new ObjectId(request.Id))) == null)
        {
            return BadRequest();
        }
        
        using var transaction = await _transactionProvider.BeginTransaction();
        competence.Descripton = request.Descripton;
        competence.Compentences = request.Compentences;
        Competence updatedCompetence = await _service.Update(competence);
        await transaction.CommitAsync();
        
        return Ok(_mapper.Map<CompetenceDTO>(updatedCompetence));
    }
    
    /// <summary>
    ///     Returns all competences for the post with the given id.
    /// </summary>
    /// <param name="postId">id of an existing subject</param>
    /// <returns>List of competences, may be empty</returns>
    [HttpGet]
    [Route("getCompetenceForSubject")]
    public async Task<ActionResult<IReadOnlyCollection<CompetenceDTO>>> GetCompetencesForSubject(string postId)
    {
        Subject? subject;
        if (string.IsNullOrWhiteSpace(postId) ||
            (subject = await _subService.GetSubjectById(new(postId))) == null)
        {
            return BadRequest();
        }

        IReadOnlyCollection<Competence>? competences = await _service.GetCompetencesForSubject(subject);
        return Ok(_mapper.Map<List<CompetenceDTO>>(competences));
    }
}