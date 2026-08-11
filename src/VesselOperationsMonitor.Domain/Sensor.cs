namespace VesselOperationsMonitor.Domain;

public sealed class Sensor
{
    public Guid Id { get; init; }

    public required string Name { get; init; }

    public SensorType Type { get; init; }

    public required string Unit { get; init; }

    public bool IsActive { get; private set; } = true;

    public void Disable()
    {
        IsActive = false;
    }

    public void Enable()
    {
        IsActive = true;
    }
}