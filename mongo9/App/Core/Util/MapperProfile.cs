using AutoMapper;
using MongoDBDemoApp.Core.Workloads.Competence;
using MongoDBDemoApp.Core.Workloads.Exam;
using MongoDBDemoApp.Core.Workloads.Student;
using MongoDBDemoApp.Core.Workloads.Subject;
using MongoDBDemoApp.Core.Workloads.Teacher;
using MongoDBDemoApp.Model.Competence;
using MongoDBDemoApp.Model.Exam;
using MongoDBDemoApp.Model.Student;
using MongoDBDemoApp.Model.Subject;
using MongoDBDemoApp.Model.Teacher;

namespace MongoDBDemoApp.Core.Util;

public sealed class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Student, StudentDTO>()
            .ForMember(p => p.Id, c => c.MapFrom(p => p.Id.ToString()));
        CreateMap<Teacher, TeacherDTO>()
            .ForMember(p => p.Id, c => c.MapFrom(p => p.Id.ToString()));
        CreateMap<Exam, ExamDTO>()
            .ForMember(p => p.Id, c => c.MapFrom(p => p.Id.ToString()));
        CreateMap<Subject, SubjectDTO>()
            .ForMember(p => p.Id, c => c.MapFrom(p => p.Id.ToString()));
        CreateMap<Competence, CompetenceDTO>()
            .ForMember(p => p.Id, c => c.MapFrom(p => p.Id.ToString()));
    }
}