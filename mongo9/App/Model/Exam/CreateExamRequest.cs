namespace MongoDBDemoApp.Model.Exam;

public class CreateExamRequest
{
    public string studentId { get; set; } = default!;
    public string teacherId { get; set; } = default!;
    public string subjectId { get; set; } = default!;
    public DateTime date { get; set; } = default!;
    public int attempt { get; set; } = default!;
}