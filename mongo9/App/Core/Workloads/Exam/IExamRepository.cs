using MongoDB.Bson;
using MongoDBDemoApp.Model.Exam;

namespace MongoDBDemoApp.Core.Workloads.Exam;

public interface IExamRepository
{
    Task<Exam> AddExam(Exam comment);
    Task<Exam?> GetExamById(ObjectId id);
    Task<IReadOnlyCollection<Exam>> GetAllExam();
    Task DeleteExam(ObjectId examId);
    Task<IReadOnlyCollection<MostParkingLots>> GetParkingLots();
}