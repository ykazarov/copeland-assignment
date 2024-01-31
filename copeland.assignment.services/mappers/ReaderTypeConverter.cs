using AutoMapper;
using copeland.assignment.models.enums;

namespace copeland.assignment.services.mappers;

public class ReaderTypeConverter : ITypeConverter<string, ReaderTypeEnum>, IValueConverter<string, ReaderTypeEnum>
{
    public ReaderTypeEnum Convert(string sourceMember, ResolutionContext context)
    {
        return Convert(sourceMember);
    }

    public ReaderTypeEnum Convert(string source, ReaderTypeEnum destination, ResolutionContext context)
    {
        return Convert(source);
    }
    
    private ReaderTypeEnum Convert(string source)
    {
        return source switch
        {
            "Temperature" => ReaderTypeEnum.Temperature,
            "TEMP" => ReaderTypeEnum.Temperature,
            "Humidity" => ReaderTypeEnum.Humidity,
            "HUM" => ReaderTypeEnum.Humidity,
            _ => ReaderTypeEnum.Unknown
        };
    }
}