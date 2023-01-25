using LeoMongo.Database;

namespace MongoDBDemoApp.Core.Workloads.Exam;

public class Exam: EntityBase
{
    public Student.Student Student { get; set; } = default!;
    public Teacher.Teacher Teacher { get; set; } = default!;
    
    
}