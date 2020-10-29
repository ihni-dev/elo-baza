using AutoMapper;
using EloBaza.Application.Commands.SubjectAggregate.Category.Create;
using EloBaza.Application.Commands.SubjectAggregate.Category.Update;
using EloBaza.Application.Commands.SubjectAggregate.Create;
using EloBaza.Application.Commands.SubjectAggregate.ExamSession.Create;
using EloBaza.Application.Commands.SubjectAggregate.ExamSession.Update;
using EloBaza.Application.Commands.SubjectAggregate.Update;
using EloBaza.Application.Queries.SubjectAggregate.Category.GetAll;
using EloBaza.Application.Queries.SubjectAggregate.ExamSession.GetAll;
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
            CreateMap<ExamSessionFilteringParametersModel, ExamSessionFilteringParameters>();

            CreateMap<CreateCategoryModel, CreateCategoryData>();
            CreateMap<UpdateCategoryModel, UpdateCategoryData>();
            CreateMap<CategoryFilteringParametersModel, CategoryFilteringParameters>();
        }
    }
}
