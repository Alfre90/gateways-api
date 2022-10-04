using AutoMapper;
using Gateways.Core.Entities;
using Gateways.Services.Gateways.Models;

namespace Gateways.Services.Gateways
{
    public class GatewaysAutomapper : Profile
    {
        public GatewaysAutomapper()
        {
            CreateMap<AddGatewayDto, Gateway>();

            CreateMap<Gateway, GatewayDto>().ReverseMap();

            CreateMap<UpdateGatewayDto, Gateway>();
        }
    }
}
