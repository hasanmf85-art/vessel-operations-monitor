namespace VesselOperationsMonitor.Domain.Tests;

public class SensorReadingTests
{
    [Fact]
    public void Should_create_sensor_reading()
    {
        // Arrange
        var sensorId = Guid.NewGuid();
        var timestamp = DateTimeOffset.UtcNow;

        // Act
        var reading = new SensorReading
        {
            Id = Guid.NewGuid(),
            SensorId = sensorId,
            Value = 87.4m,
            Timestamp = timestamp
        };

        // Assert
        Assert.Equal(sensorId, reading.SensorId);
        Assert.Equal(87.4m, reading.Value);
        Assert.Equal(timestamp, reading.Timestamp);
    }

    [Fact]
    public void should_disbale_sensor()
    {
        var sensor = new Sensor()
        {
            Name = "Engine Temperature Sensor",
            Type = SensorType.Temperature,
            Unit = "C",
        };

        Assert.True(sensor.IsActive);
        sensor.Disable();
        Assert.False(sensor.IsActive);
    }
}