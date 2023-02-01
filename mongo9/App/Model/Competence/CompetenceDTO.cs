using MongoDB.Bson;

namespace MongoDBDemoApp.Model.Competence;

public class CompetenceDTO
{
    public string Id { get; set; } = default!;
    public string Compentences { get; set; } = default!;
    public string Descripton { get; set; } = default!;
    public string SubjectId { get; set; } = default!;
}