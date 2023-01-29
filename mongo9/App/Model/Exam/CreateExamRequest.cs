namespace MongoDBDemoApp.Model.Exam;

public class CreateExamRequest
{
    public string TeacherId { get; set; } = default!;
    public string StudentId { get; set; } = default!;
    public string SubjectId { get; set; } = default!;
    public int Grade { get; set; } = default!;
    public bool PassedExam { get; set; } = default!;
    public DateTime Date { get; set; } = default!;
    public int Attempt { get; set; } = default!;
    //public List<Core.Workloads.Competence.Competence> Competence { get; set; } = default!;
}