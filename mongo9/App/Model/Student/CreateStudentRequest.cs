namespace MongoDBDemoApp.Model.Student;

public sealed class CreateStudentRequest
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
}