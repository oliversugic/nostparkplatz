using LeoMongo.Database;

namespace MongoDBDemoApp.Core.Workloads.Exam;

public class Exam: EntityBase
{
    public Student.Student Student { get; set; } = default!;
    public Teacher.Teacher Teacher { get; set; } = default!;
    public Subject.Subject Subject { get; set; } = default!;
    public int Grade { get; set; } = default!;
    public bool PassedExam { get; set; } = default!;
    public DateTime Date { get; set; } = default!;
    public int Attempt { get; set; } = default!;
    public List<Competence.Competence> Competence { get; set; } = default!;

}