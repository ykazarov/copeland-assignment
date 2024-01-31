namespace copeland.assignment.models;

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class Device
{
    public int DeviceID { get; set; }
    public string Name { get; set; }
    public DateTime StartDateTime { get; set; }
    public List<SensorDatum> SensorData { get; set; }
}

public class DeviceDataFoo2Model
{
    public int CompanyId { get; set; }
    public string Company { get; set; }
    public List<Device> Devices { get; set; }
}

public class SensorDatum
{
    public string SensorType { get; set; }
    public DateTime DateTime { get; set; }
    public double Value { get; set; }
}

