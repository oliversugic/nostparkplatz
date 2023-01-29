namespace MongoDBDemoApp.Model.Competence;

public class CreateCompetenceRequest
{
    public string Compentences { get; set; } = default!;
    public string Descripton { get; set; } = default!;
    public string SubjectId { get; set; } = default!;
}