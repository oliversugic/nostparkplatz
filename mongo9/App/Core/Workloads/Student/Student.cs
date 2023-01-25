using LeoMongo.Database;

namespace MongoDBDemoApp.Core.Workloads.Student
{
    public sealed class Student : EntityBase
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;

    }
}
