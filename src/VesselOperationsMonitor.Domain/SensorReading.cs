namespace VesselOperationsMonitor.Domain;

public sealed class SensorReading
{
    public Guid VesselId { get; }

    public Guid SensorId { get; }

    public SensorType Type { get; }

    public decimal Value { get; }

    public string Unit { get; }

    public DateTimeOffset RecordedAt { get; }

    public SensorReading(
        Guid vesselId,
        Guid sensorId,
        SensorType type,
        decimal value,
        string unit,
        DateTimeOffset recordedAt)
    {
        if (vesselId == Guid.Empty)
        {
            throw new ArgumentException(
                "Vessel ID cannot be empty.",
                nameof(vesselId));
        }

        if (sensorId == Guid.Empty)
        {
            throw new ArgumentException(
                "Sensor ID cannot be empty.",
                nameof(sensorId));
        }

        if (string.IsNullOrWhiteSpace(unit))
        {
            throw new ArgumentException(
                "Unit cannot be empty.",
                nameof(unit));
        }

        VesselId = vesselId;
        SensorId = sensorId;
        Type = type;
        Value = value;
        Unit = unit;
        RecordedAt = recordedAt;
    }
}