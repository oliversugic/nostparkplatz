using LeoMongo.Database;

namespace MongoDBDemoApp.Core.Workloads.Subject;

public class Subject: EntityBase
{
    public string Name { get; set; } = default!;
}