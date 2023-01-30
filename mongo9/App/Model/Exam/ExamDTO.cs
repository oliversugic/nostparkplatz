using MongoDB.Bson;

namespace MongoDBDemoApp.Model.Exam
{
    public class ExamDTO
    {
        public string Id { get; set; } = default!;
        //public Core.Workloads.Student.Student Student { get; set; } = default!;
        //public Core.Workloads.Teacher.Teacher Teacher { get; set; } = default!;
        //public Core.Workloads.Subject.Subject Subject { get; set; } = default!;
        public ObjectId StudentId { get; set; } = default!;
        public ObjectId TeacherId { get; set; } = default!;
        public ObjectId SubjectId { get; set; } = default!;
        public int Grade { get; set; } = default!;
        public bool PassedExam { get; set; } = default!;
        public DateTime Date { get; set; } = default!;
        public int Attempt { get; set; } = default!;
    }
}
