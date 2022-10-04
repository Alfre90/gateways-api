using AutoMapper;
using Gateways.Core.Entities;
using Gateways.Services.Devices.Models;

namespace Gateways.Services.Devices
{
    public class DevicesAutomapper : Profile
    {
        public DevicesAutomapper()
        {
            CreateMap<AddDeviceDto, Device>()
                .ForMember(dest => dest.Created, opt => opt.MapFrom(source => DateTime.Now));

            CreateMap<Device, DeviceDto>().ReverseMap();

            CreateMap<UpdateDeviceDto, Device>();
        }
    }
}
