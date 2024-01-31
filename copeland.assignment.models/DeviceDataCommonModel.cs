using copeland.assignment.models.enums;

namespace copeland.assignment.models;

public class Reading
{
    public DateTime CreatedDt { get; set; }
    public double Value { get; set; }
}

public class DeviceDataCommonModel
{
    public int SourceId { get; set; }
    public string SourceName { get; set; }
    public List<Equipment> Devices { get; set; }
}

public class Reader
{
    public int Id { get; set; }
    
    public ReaderTypeEnum Type { get; set; }
    public List<Reading> Readings { get; set; }
}

public class Equipment
{
    public int Id { get; set; }
    public string Model { get; set; }
    public DateTime ShippedDt { get; set; }
    public List<Reader> Readers { get; set; }
}