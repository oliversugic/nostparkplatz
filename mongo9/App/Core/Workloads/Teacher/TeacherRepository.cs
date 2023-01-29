using LeoMongo;
using LeoMongo.Database;
using LeoMongo.Transaction;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace MongoDBDemoApp.Core.Workloads.Teacher;

public sealed class TeacherRepository: RepositoryBase<Teacher>, ITeacherRepository
{
    public TeacherRepository(ITransactionProvider transactionProvider, IDatabaseProvider databaseProvider) : base(transactionProvider, databaseProvider)
    {
    }

    public override string CollectionName { get; } =  MongoUtil.GetCollectionName<Teacher>();
    public async Task<Teacher> AddTeacher(Teacher teacher)
    {
        return await this.InsertOneAsync(teacher);
    }

    public async Task<Teacher?> GetTeacherById(ObjectId id)
    {
        var c = await this.Query().Where(g => g.Id == id).FirstAsync();
        return c;
    }

    public async Task<IReadOnlyCollection<Teacher>> GetAllTeachers()
    {
        return await this.Query().ToListAsync();
    }

    public Task DeleteTeacher(ObjectId teacherId)
    {
        return this.DeleteOneAsync(teacherId);
    }

    public async Task<Teacher> Update(Teacher teacher)
    {
        await this.UpdateOneAsync(teacher.Id, 
            Builders<Teacher>.Update.Set(p => p.FirstName, teacher.FirstName));
        await this.UpdateOneAsync(teacher.Id,
            Builders<Teacher>.Update.Set(p => p.LastName, teacher.LastName));
        Teacher updated = await Query().Where(g => g.Id == teacher.Id).FirstAsync();
        return updated;
    }
}