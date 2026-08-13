namespace VesselOperationsMonitor.Domain;

public sealed class Sensor
{
    public Guid Id { get; }
    
    public Guid VesselId { get; }

    public string Name { get; init; }

    public SensorType Type { get; init; }

    public string Unit { get; init; }

    public bool IsActive { get; private set; } = true;

    public Sensor(
        Guid vesselId,
        string name,
        SensorType type,
        string unit)
    {
        if (vesselId == Guid.Empty)
        {
            throw new ArgumentException(
                "Vessel ID cannot be empty.",
                nameof(vesselId));
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException(
                "Sensor name cannot be empty.",
                nameof(name));
        }

        if (string.IsNullOrWhiteSpace(unit))
        {
            throw new ArgumentException(
                "Sensor unit cannot be empty.",
                nameof(unit));
        }

        Id = Guid.NewGuid();
        VesselId = vesselId;
        Name = name;
        Type = type;
        Unit = unit;
    }

    public void Disable()
    {
        IsActive = false;
    }

    public void Enable()
    {
        IsActive = true;
    }
}