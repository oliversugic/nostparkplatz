using AutoMapper;
using LeoMongo.Transaction;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDBDemoApp.Core.Util;
using MongoDBDemoApp.Core.Workloads.Competence;
using MongoDBDemoApp.Core.Workloads.Student;
using MongoDBDemoApp.Model.Competence;
using MongoDBDemoApp.Model.Student;

namespace MongoDBDemoApp.Controllers;


[Route("api/[controller]")]
[ApiController]
public sealed class CompetenceController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICompetenceService _service;
    private readonly ITransactionProvider _transactionProvider;

    public CompetenceController(IMapper mapper, ICompetenceService service, ITransactionProvider transactionProvider)
    {
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
            || string.IsNullOrWhiteSpace(request.Descripton))
        {
            return BadRequest();
        }

        using var transaction = await _transactionProvider.BeginTransaction();
        Competence competence = await _service.AddCompetence(request.Compentences, request.Descripton);
        await transaction.CommitAsync();
        return CreatedAtAction(nameof(GetById), new { id = competence.Id.ToString() }, competence);
    }
}