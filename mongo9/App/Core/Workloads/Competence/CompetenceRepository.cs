using LeoMongo;
using LeoMongo.Database;
using LeoMongo.Transaction;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using SharpCompress.Common;

namespace MongoDBDemoApp.Core.Workloads.Competence;

public sealed class CompetenceRepository: RepositoryBase<Competence>, ICompetenceRepository
{
    public CompetenceRepository(ITransactionProvider transactionProvider, 
        IDatabaseProvider databaseProvider) : base(transactionProvider, databaseProvider)
    {
    }

    public override string CollectionName { get; } =  MongoUtil.GetCollectionName<Competence>();
    
    public async Task<Competence> AddCompetence(Competence comment)
    {
        return await InsertOneAsync(comment);
    }

    public async Task<Competence?> GetCompetenceById(ObjectId id)
    {
        var c = await Query().Where(g => g.Id == id).FirstAsync();
        return c;
    }

    public async Task<IReadOnlyCollection<Competence>> GetAllCompetences()
    {
        return await Query().ToListAsync();
    }

    public Task DeleteCompetence(ObjectId postId)
    {
        return this.DeleteOneAsync(postId);
    }

    public async Task<Competence> Update(Competence competence)
    {
        await this.UpdateOneAsync(competence.Id, 
            Builders<Competence>.Update.Set(p => p.Compentences, competence.Compentences));
        await this.UpdateOneAsync(competence.Id,
            Builders<Competence>.Update.Set(p => p.Descripton, competence.Descripton));
        Competence updated = await Query().Where(g => g.Id == competence.Id).FirstAsync();
        return updated;
    }
}