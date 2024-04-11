using AutoMapper;
using Web.BFF.API.Endpoints.v1.Models.Configuration;
using Web.BFF.Domain.Configuration;

namespace Web.BFF.API.Endpoints.v1
{
    public class ApiV1MapperProfile : Profile
    {
        public ApiV1MapperProfile()
        {
            CreateMap<WebAppConfiguration, WebAppConfigurationResponse>();
        }
    }
}
