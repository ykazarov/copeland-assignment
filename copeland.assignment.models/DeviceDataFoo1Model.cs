namespace copeland.assignment.models;

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class Crumb
{
    public DateTime CreatedDtm { get; set; }
    public double Value { get; set; }
}

public class DeviceDataFoo1Model
{
    public int PartnerId { get; set; }
    public string PartnerName { get; set; }
    public List<Tracker> Trackers { get; set; }
}

public class Sensor
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Crumb> Crumbs { get; set; }
}

public class Tracker
{
    public int Id { get; set; }
    public string Model { get; set; }
    public DateTime ShipmentStartDtm { get; set; }
    public List<Sensor> Sensors { get; set; }
}
