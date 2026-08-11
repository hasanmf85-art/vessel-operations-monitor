namespace VesselOperationsMonitor.Domain;

public sealed class SensorReading
{
    public Guid Id { get; init; }

    public Guid SensorId { get; init; }

    public decimal Value { get; init; }

    public DateTimeOffset Timestamp { get; init; }
}