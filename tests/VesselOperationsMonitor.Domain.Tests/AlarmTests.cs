using Xunit.Sdk;

namespace VesselOperationsMonitor.Domain.Tests;

public class AlarmTests
{
    [Fact]
    public void should_create_unacknowledged_alarm()
    {
        var vesselId = Guid.NewGuid();
        var sensorId = Guid.NewGuid();
        var createdAt = new DateTimeOffset(
            2026, 8, 13, 8, 0, 0,
            TimeSpan.Zero);

        var alarm = new Alarm(
            vesselId,
            sensorId,
            "Engine temperature is to high",
            AlarmSeverity.Warning,
            80M,
            createdAt
            );
        
        Assert.NotEqual(Guid.Empty, alarm.Id);
        Assert.Equal(vesselId, alarm.VesselId);
        Assert.Equal(sensorId, alarm.SensorId);
        Assert.Equal("Engine temperature is to high", alarm.Message);
        Assert.Equal(AlarmSeverity.Warning, alarm.Severity);
        Assert.Equal(80M, alarm.TriggerValue);
        Assert.Equal(createdAt, alarm.CreatedAt);
        
        Assert.False(alarm.IsAcknowledged);
        Assert.Null(alarm.AcknowledgeBy);
        Assert.Null(alarm.AcknowledgementComment);
        Assert.Null(alarm.AcknowledgedAt);
        
    }

    [Fact]
    public void should_acknowledge_alarm()
    {
        var createdAt = new DateTimeOffset(
            2026, 8, 13, 8, 0, 0,
            TimeSpan.Zero);
        var alarm = CreateAlarm(createdAt);

        var acknowledgeTime = createdAt.AddMinutes(3);
        
        alarm.Acknowledge("Hasan", "Engine checked", acknowledgeTime);
        
        Assert.True(alarm.IsAcknowledged);
        Assert.Equal("Hasan", alarm.AcknowledgeBy);
        Assert.Equal("Engine checked", alarm.AcknowledgementComment);
        Assert.Equal(createdAt ,alarm.CreatedAt);
    }

    [Fact]
    public void should_not_create_another_acknowledgement_of_same_alarm()
    {
        var createdAt = new DateTimeOffset(
            2026, 8, 13, 8, 0, 0,
            TimeSpan.Zero);
        var alarm = CreateAlarm(createdAt);
        var acknowledgeTime = createdAt.AddMinutes(3);
        alarm.Acknowledge("Hasan", "Engine restarted", acknowledgeTime);
        
        var secondAcknowledgeTime = acknowledgeTime.AddMinutes(3);


        var exception = Assert.Throws<InvalidOperationException>(() => alarm.Acknowledge("User X", "Engine stopped", secondAcknowledgeTime));
        Assert.Equal("Alarm already acknowledged", exception.Message);
        Assert.NotEqual("User X", alarm.AcknowledgeBy);
        Assert.NotEqual("Engine stopped", alarm.AcknowledgementComment);
        
    }

    [Fact]
    public void should_reject_acknowledgement_created_before_alarm()
    {
        var createdAt = new DateTimeOffset(
            2026, 8, 13, 8, 0, 0,
            TimeSpan.Zero);
        var alarm = CreateAlarm(createdAt);
        var acknowledgeTime = createdAt.AddMinutes(-3);
        
        
        var exception = Assert.Throws<ArgumentException>(() => alarm.Acknowledge("Hasan", "Engine restarted", acknowledgeTime));
        
        Assert.False(alarm.IsAcknowledged);
        Assert.Equal("at", exception.ParamName);

        
    }

    private static Alarm CreateAlarm(DateTimeOffset createdAt)
    {
       return new Alarm(
           Guid.NewGuid(),
            Guid.NewGuid(),
            "Engine temperature is too high.",
            AlarmSeverity.Critical,
            110.5m,
            createdAt);
    }
}