using AutoMapper;
using EloBaza.Application.Commands.ExamSession.Create;
using EloBaza.Application.Commands.ExamSession.Update;
using EloBaza.Application.Commands.Subject.Create;
using EloBaza.Application.Commands.Subject.Update;
using EloBaza.Application.Queries.Subject.GetAll;

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
