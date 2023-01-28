namespace MongoDBDemoApp.Model.Teacher;

public sealed class CreateTeacherRequest
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
}