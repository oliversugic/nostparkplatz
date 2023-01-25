using LeoMongo.Database;
using LeoMongo.Transaction;
using MongoDB.Bson;

namespace MongoDBDemoApp.Core.Workloads.Competence;

public sealed class CompetenceRepository: RepositoryBase<Competence>, ICompetenceRepository
{
    public CompetenceRepository(ITransactionProvider transactionProvider, IDatabaseProvider databaseProvider) : base(transactionProvider, databaseProvider)
    {
    }

    public override string CollectionName { get; } = default!;
    
    public Task<Competence> AddCompetence(Competence comment)
    {
        throw new NotImplementedException();
    }

    public Task<Competence?> GetCompetenceById(ObjectId id)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<Competence>> GetAllCompetences()
    {
        throw new NotImplementedException();
    }

    public Task DeleteCompetence(ObjectId postId)
    {
        throw new NotImplementedException();
    }
}