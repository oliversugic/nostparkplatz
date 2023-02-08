using MongoDB.Bson;
using MongoDBDemoApp.Model.Exam;

namespace MongoDBDemoApp.Core.Workloads.Exam;

public interface IExamService
{
    Task<IReadOnlyCollection<Exam>> GetAllExams();
    Task<Exam?> GetExamById(ObjectId objectId);
    Task<Exam> AddExam(bool requestPassedExam, int requestAttempt, DateTime requestDate, int requestGrade, string requestStudentId, string requestTeacherId, string requestSubjectId);
    Task<IReadOnlyCollection<MostParkingLots>> GetAllParkingLots();
}