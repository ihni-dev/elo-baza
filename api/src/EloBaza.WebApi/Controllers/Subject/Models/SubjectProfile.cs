using AutoMapper;
using EloBaza.Application.Commands.Create;
using EloBaza.Application.Commands.Update;
using EloBaza.Application.Queries.Subject;

namespace EloBaza.WebApi.Controllers.Subject.Models
{
    public class SubjectProfile : Profile
    {
        public SubjectProfile()
        {
            CreateMap<CreateSubjectModel, CreateSubjectData>();
            CreateMap<UpdateSubjectModel, UpdateSubjectData>();
            CreateMap<SubjectFilteringParametersModel, SubjectFilteringParameters>();
        }
    }
}
