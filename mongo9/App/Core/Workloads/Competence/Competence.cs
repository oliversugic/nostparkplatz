using LeoMongo.Database;
using MongoDB.Bson;

namespace MongoDBDemoApp.Core.Workloads.Competence;

public class Competence: EntityBase
{
    public string Compentences { get; set; } = default!;
    public string Descripton { get; set; } = default!;
    public ObjectId SubjectId { get; set; }
}