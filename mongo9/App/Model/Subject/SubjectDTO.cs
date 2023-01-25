namespace MongoDBDemoApp.Model.Subject;

public class SubjectDTO
{
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public List<Core.Workloads.Competence.Competence> Competences { get; set; } = default!;
}