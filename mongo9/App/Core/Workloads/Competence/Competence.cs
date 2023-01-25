using LeoMongo.Database;

namespace MongoDBDemoApp.Core.Workloads.Competence;

public class Competence: EntityBase
{
    public string Compentences { get; set; } = default!;
    public string Descripton { get; set; } = default!;
}