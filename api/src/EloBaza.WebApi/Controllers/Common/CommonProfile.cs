using AutoMapper;
using EloBaza.Application.Queries.Common;

namespace EloBaza.WebApi.Controllers.Common
{
    public class CommonProfile : Profile
    {
        public CommonProfile()
        {
            CreateMap<PagingParametersModel, PagingParameters>();
        }
    }
}
