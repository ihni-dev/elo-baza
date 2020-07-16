using AutoMapper;
using EloBaza.Application.Commands.SubjectAggregate.Create;
using EloBaza.Application.Commands.SubjectAggregate.ExamSession.Create;
using EloBaza.Application.Commands.SubjectAggregate.ExamSession.Update;
using EloBaza.Application.Commands.SubjectAggregate.Update;
using EloBaza.Application.Queries.SubjectAggregate.GetAll;

namespace EloBaza.WebApi.Controllers.Subject.Models
{
    public class SubjectProfile : Profile
    {
        public SubjectProfile()
        {
            CreateMap<CreateSubjectModel, CreateSubjectData>();
            CreateMap<UpdateSubjectModel, UpdateSubjectData>();
            CreateMap<SubjectFilteringParametersModel, SubjectFilteringParameters>();

            CreateMap<CreateExamSessionModel, CreateExamSessionData>();
            CreateMap<UpdateExamSessionModel, UpdateExamSessionData>();
        }
    }
}
