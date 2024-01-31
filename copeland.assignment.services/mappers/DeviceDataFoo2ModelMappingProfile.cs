using AutoMapper;
using copeland.assignment.models;
using copeland.assignment.models.enums;

namespace copeland.assignment.services.mappers;

public class DeviceDataFoo2ModelMappingProfile : Profile
{
    public DeviceDataFoo2ModelMappingProfile()
    {
        CreateMap<SensorDatum, Reading>()
            .ForMember(dest => dest.CreatedDt, opt => opt.MapFrom(src => src.DateTime))
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value));

        CreateMap<Device, List<Reader>>().ConvertUsing<DeviceToReadersConverter>();

        CreateMap<Device, Equipment>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.DeviceID))
            .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.ShippedDt, opt => opt.MapFrom(src => src.StartDateTime))
            .ForMember(dest => dest.Readers, opt => opt.MapFrom(src => src));
        
        CreateMap<DeviceDataFoo2Model, DeviceDataCommonModel>()
            .ForMember(dest => dest.SourceId, opt => opt.MapFrom(src => src.CompanyId))
            .ForMember(dest => dest.SourceName, opt => opt.MapFrom(src => src.Company))
            .ForMember(dest => dest.Devices, opt => opt.MapFrom(src => src.Devices));
    }
}

public class DeviceToReadersConverter : ITypeConverter<Device, List<Reader>>
{
    public List<Reader> Convert(Device device, List<Reader> destination, ResolutionContext context)
    {
        var readerTypeConverter = new ReaderTypeConverter();
        
       var readers = device.SensorData
           .Select(sd => sd.SensorType)
           .Distinct()
           .Select(readerName => new Reader()
            {
                Id = 0,
                Type = readerTypeConverter.Convert(readerName, null)
            })
            .ToList();
       
       readers.ForEach(r => r.Readings = device.SensorData
           .Where(sd => r.Type == readerTypeConverter.Convert(sd.SensorType, null))
           .Select(sd => new Reading()
           {
               CreatedDt = sd.DateTime,
               Value = sd.Value
           }).ToList());
       
       return readers;
    }
}
