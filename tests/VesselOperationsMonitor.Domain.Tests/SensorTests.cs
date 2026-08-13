namespace VesselOperationsMonitor.Domain.Tests;

public class SensorTests
{
    [Fact]
    public void Should_create_active_sensor_for_vessel()
    {
        // Arrange
        var vesselId = Guid.NewGuid();

        // Act
        var sensor = new Sensor(
            vesselId,
            "Main Engine Temperature",
            SensorType.Temperature,
            "°C");

        // Assert
        Assert.NotEqual(Guid.Empty, sensor.Id);
        Assert.Equal(vesselId, sensor.VesselId);
        Assert.Equal("Main Engine Temperature", sensor.Name);
        Assert.Equal(SensorType.Temperature, sensor.Type);
        Assert.Equal("°C", sensor.Unit);
        Assert.True(sensor.IsActive);
    }

    [Fact]
    public void Should_disable_sensor()
    {
        // Arrange
        var sensor = CreateSensor();

        // Act
        sensor.Disable();

        // Assert
        Assert.False(sensor.IsActive);
    }

    [Fact]
    public void Should_enable_disabled_sensor()
    {
        // Arrange
        var sensor = CreateSensor();
        sensor.Disable();

        // Act
        sensor.Enable();

        // Assert
        Assert.True(sensor.IsActive);
    }

    [Fact]
    public void Should_reject_empty_vessel_id()
    {
        var exception = Assert.Throws<ArgumentException>(
            () => new Sensor(
                Guid.Empty,
                "Main Engine Temperature",
                SensorType.Temperature,
                "°C"));

        Assert.Equal("vesselId", exception.ParamName);
    }

    private static Sensor CreateSensor()
    {
        return new Sensor(
            Guid.NewGuid(),
            "Main Engine Temperature",
            SensorType.Temperature,
            "°C");
    }
    
    [Fact]
    public void Should_create_sensor_reading()
    {
        // Arrange
        var vesselId = Guid.NewGuid();
        var sensorId = Guid.NewGuid();

        var recordedAt = DateTimeOffset.UtcNow;

        // Act
        var reading = new SensorReading(
            vesselId,
            sensorId,
            SensorType.Pressure,
            87.4m,
            "m",
            recordedAt
        );

        // Assert
        Assert.Equal(vesselId, reading.VesselId);
        Assert.Equal(SensorType.Pressure, reading.Type);
        Assert.Equal(87.4m, reading.Value);
        Assert.Equal("m", reading.Unit);
        Assert.Equal(recordedAt, reading.RecordedAt);
    }

}
