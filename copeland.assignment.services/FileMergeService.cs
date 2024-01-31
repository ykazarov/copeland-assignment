using AutoMapper;
using copeland.assignment.core;
using copeland.assignment.models;
using copeland.assignment.models.enums;

namespace copeland.assignment.services;

public class FileMergeService : IFileMergeService<DeviceDataTableModel>
{
    private readonly IFileIOService<DeviceDataFoo1Model> _reader1;
    private readonly IFileIOService<DeviceDataFoo2Model> _reader2;
    private readonly IMapper _mapper;
    private readonly IFileIOService<DeviceDataTableModel> _tableWriter;

    public FileMergeService(
        IFileIOService<DeviceDataFoo1Model> reader1, 
        IFileIOService<DeviceDataFoo2Model> reader2,
        IFileIOService<DeviceDataTableModel> tableWriter,
        IMapper mapper)
    {
        _reader1 = reader1 ?? throw new ArgumentNullException(nameof(reader1));
        _reader2 = reader2 ?? throw new ArgumentNullException(nameof(reader2));
        _tableWriter = tableWriter ?? throw new ArgumentNullException(nameof(tableWriter));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    
    public void Merge<T>(string file1, string file2, string outputFile)
    {
        var file1Contents = _reader1.ReadFromFile(file1);
        var file2Contents = _reader2.ReadFromFile(file2);
        
        var standardModel1 = _mapper.Map<DeviceDataCommonModel>(file1Contents);
        var standardModel2 = _mapper.Map<DeviceDataCommonModel>(file2Contents);

        var destination = new DeviceDataTableModel();
        
        AggregateIntoTable(standardModel1, ref destination);
        AggregateIntoTable(standardModel2, ref destination);
        
        _tableWriter.WriteToFile(outputFile, destination);
    }
    
    private void AggregateIntoTable(DeviceDataCommonModel standardModel, ref DeviceDataTableModel destination)
    {
        
        var range = standardModel.Devices
            .SelectMany(d => d.Readers
                .SelectMany(r => r.Readings
                    .Select(reading => CreateRecord(standardModel, d))));

        var uniqueRange = range.Distinct();
        
        destination.Records.AddRange(uniqueRange);
    }

    private static DeviceDataRecordModel CreateRecord(DeviceDataCommonModel standardModel, Equipment d)
    {
        var result = new DeviceDataRecordModel
        {
            CompanyId = standardModel.SourceId,
            CompanyName = standardModel.SourceName,
            DeviceId = d.Id,
            DeviceName = d.Model,
            
            AverageHumdity = AverageFor(d, ReaderTypeEnum.Humidity),
            
            HumidityCount = d.Readers
                .Where(r => r.Type == ReaderTypeEnum.Humidity)
                .SelectMany(r => r.Readings)
                .Count(),
            
            AverageTemperature = AverageFor(d, ReaderTypeEnum.Temperature),
            
            TemperatureCount = d.Readers
                .Where(r => r.Type == ReaderTypeEnum.Temperature)
                .SelectMany(r => r.Readings)
                .Count(),
            
            FirstReadingDtm = d.Readers.SelectMany(r => r.Readings).Min(r => r.CreatedDt),
            LastReadingDtm = d.Readers.SelectMany(r => r.Readings).Max(r => r.CreatedDt)
        };

        return result;
    }

    private static double AverageFor(Equipment d, ReaderTypeEnum readerType)
    {
        var readings = d.Readers
            .Where(r => r.Type == readerType)
            .SelectMany(r => r.Readings);
            
            if (readings.Count() > 0)
            {
                return readings.Average(reading => reading.Value);
            }

            return 0;
    }
}