using LeoMongo.Database;

namespace MongoDBDemoApp.Core.Workloads.Teacher
{
    public class Teacher: EntityBase
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
    }
}
