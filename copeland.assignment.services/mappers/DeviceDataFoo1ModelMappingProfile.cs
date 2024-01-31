using AutoMapper;
using copeland.assignment.models;
using copeland.assignment.models.enums;

namespace copeland.assignment.services.mappers;

public class DeviceDataFoo1ModelMappingProfile : Profile
{
    public DeviceDataFoo1ModelMappingProfile()
    {
        CreateMap<string, ReaderTypeEnum>()
            .ConvertUsing<ReaderTypeConverter>();
        
        CreateMap<Crumb, Reading>()
            .ForMember(dest => dest.CreatedDt, opt => opt.MapFrom(src => src.CreatedDtm))
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value));
        
        CreateMap<Sensor, Reader>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Readings, opt => opt.MapFrom(src => src.Crumbs));
        
        CreateMap<Tracker, Equipment>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model))
            .ForMember(dest => dest.ShippedDt, opt => opt.MapFrom(src => src.ShipmentStartDtm))
            .ForMember(dest => dest.Readers, opt => opt.MapFrom(src => src.Sensors));
        
        CreateMap<DeviceDataFoo1Model, DeviceDataCommonModel>()
            .ForMember(dest => dest.SourceId, opt => opt.MapFrom(src => src.PartnerId))
            .ForMember(dest => dest.SourceName, opt => opt.MapFrom(src => src.PartnerName))
            .ForMember(dest => dest.Devices, opt => opt.MapFrom(src => src.Trackers));
    }
}