using LeoMongo;
using LeoMongo.Database;
using LeoMongo.Transaction;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDBDemoApp.Core.Util;
using MongoDBDemoApp.Core.Workloads.Competence;

namespace MongoDBDemoApp.Core.Workloads.Subject;

public class SubjectRepository: RepositoryBase<Subject>,ISubjectRepository
{
    private readonly ICompetenceRepository _competenceRepository;
    public SubjectRepository(ITransactionProvider transactionProvider,
        IDatabaseProvider databaseProvider, ICompetenceRepository competenceRepository) : base(transactionProvider, databaseProvider)
    {
        _competenceRepository = competenceRepository;
    }

    public override string CollectionName { get; } = MongoUtil.GetCollectionName<Subject>();
    
    public async Task<Subject> AddSubject(Subject comment)
    {
        return await this.InsertOneAsync(comment);
    }

    public async Task<Subject?> GetSubjectById(ObjectId id)
    {
        var c = await Query().Where(g => g.Id == id).FirstAsync();
        return c;
    }

    public async Task<IReadOnlyCollection<Subject>> GetAllSubject()
    {
        return await Query().ToListAsync();
    }

    public Task DeleteSubject(ObjectId postId)
    {
        return this.DeleteOneAsync(postId);
    }

    public async Task<(ObjectId SubjectId, List<ObjectId>? CompetenceIds)?> GetCompetencesWithSubject(ObjectId subjectId)
    {
        IDictionary<ObjectId, List<ObjectId>?> subjectWithCompetences = await QueryIncludeDetail<Competence.Competence>(
                _competenceRepository,
                c => c.SubjectId, p => p.Id == subjectId)
            .ToDictionaryAsync();
        if (subjectWithCompetences.Count == 0
            || subjectWithCompetences.Keys.All(p => p != subjectId))
        {
            return null;
        }

        return subjectWithCompetences.First().ToTuple();
    }

    public async Task<Subject> Update(Subject subject)
    {
        await this.UpdateOneAsync(subject.Id, 
                        Builders<Subject>.Update.Set(p => p.Name, subject.Name));
        Subject updated = await Query().Where(g => g.Id == subject.Id).FirstAsync();
        return updated;
    }
}