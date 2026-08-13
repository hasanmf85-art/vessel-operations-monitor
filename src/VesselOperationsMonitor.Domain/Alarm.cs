namespace VesselOperationsMonitor.Domain;

public class Alarm
{
    public Guid Id { get; init; }
    public Guid VesselId { get; }
    public Guid SensorId { get; }
    public AlarmSeverity Severity { get; }
    public decimal TriggerValue { get; }
    public DateTimeOffset CreatedAt { get; }
    public string Message { get; }

    public string? AcknowledgeBy { get; private set; }
    public DateTimeOffset? AcknowledgedAt { get; private set; }
    public string? AcknowledgementComment { get; private set; }

    public bool IsAcknowledged => AcknowledgedAt is { } offset;


    public Alarm(Guid vesselId, Guid sensorId, string message, AlarmSeverity severity, decimal triggerValue,
        DateTimeOffset createdAt)
    {
        if (vesselId == Guid.Empty) throw new ArgumentException("Vessel ID cannot be empty", nameof(vesselId));
        if (sensorId == Guid.Empty) throw new ArgumentException("Sensor ID cannot be empty", nameof(sensorId));
        if (string.IsNullOrWhiteSpace(message)) throw new ArgumentException("Message cannot be empty", nameof(message));

        Id = Guid.NewGuid();
        VesselId = vesselId;
        SensorId = sensorId;
        Message = message;
        Severity = severity;
        TriggerValue = triggerValue;
        CreatedAt = createdAt;
    }


    public void Acknowledge(string by, string comment, DateTimeOffset at)
    {
        // input params is valid, then change isAcknowledged value

        if (IsAcknowledged) throw new InvalidOperationException("Alarm already acknowledged");

        if (string.IsNullOrWhiteSpace(by)) throw new ArgumentException("By cannot be empty", nameof(by));
        if (string.IsNullOrWhiteSpace(by)) throw new ArgumentException("Comment cannot be empty", nameof(by));
        if (at < CreatedAt) throw new ArgumentException("Acknowledgement time cannot be earlier than", nameof(at));

        AcknowledgeBy = by;
        AcknowledgedAt = at;
        AcknowledgementComment = comment;
    }
}