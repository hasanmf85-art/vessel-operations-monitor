using VesselOperationsMonitor.Domain;

namespace VesselOperationsMonitor.Application.Tests;

public sealed class AlarmEvaluatorTests
{
    private readonly AlarmEvaluator _evaluator = new();

    [Fact]
    public void Should_create_critical_alarm_for_critical_temperature()
    {
        // Arrange
        var reading = CreateReading(
            SensorType.Temperature,
            108.5m,
            "°C");

        // Act
        var alarm = _evaluator.Evaluate(reading);

        // Assert
        Assert.NotNull(alarm);
        Assert.Equal(AlarmSeverity.Critical, alarm.Severity);
        Assert.Equal(reading.VesselId, alarm.VesselId);
        Assert.Equal(reading.SensorId, alarm.SensorId);
        Assert.Equal(reading.Value, alarm.TriggerValue);
        Assert.Equal(
            "Engine temperature is critically high.",
            alarm.Message);
    }

    [Fact]
    public void Should_create_warning_alarm_for_high_temperature()
    {
        // Arrange
        var reading = CreateReading(
            SensorType.Temperature,
            98m,
            "°C");

        // Act
        var alarm = _evaluator.Evaluate(reading);

        // Assert
        Assert.NotNull(alarm);
        Assert.Equal(AlarmSeverity.Warning, alarm.Severity);
    }

    [Fact]
    public void Should_create_critical_alarm_for_critically_low_pressure()
    {
        // Arrange
        var reading = CreateReading(
            SensorType.Pressure,
            1.8m,
            "bar");

        // Act
        var alarm = _evaluator.Evaluate(reading);

        // Assert
        Assert.NotNull(alarm);
        Assert.Equal(AlarmSeverity.Critical, alarm.Severity);
        Assert.Equal(
            "Engine oil pressure is critically low.",
            alarm.Message);
    }

    [Fact]
    public void Should_create_warning_alarm_for_low_pressure()
    {
        // Arrange
        var reading = CreateReading(
            SensorType.Pressure,
            2.7m,
            "bar");

        // Act
        var alarm = _evaluator.Evaluate(reading);

        // Assert
        Assert.NotNull(alarm);
        Assert.Equal(AlarmSeverity.Warning, alarm.Severity);
    }

    [Theory]
    [InlineData(85)]
    [InlineData(94.9)]
    public void Should_not_create_alarm_for_normal_temperature(
        decimal value)
    {
        // Arrange
        var reading = CreateReading(
            SensorType.Temperature,
            value,
            "°C");

        // Act
        var alarm = _evaluator.Evaluate(reading);

        // Assert
        Assert.Null(alarm);
    }

    [Theory]
    [InlineData(3.1)]
    [InlineData(5.5)]
    public void Should_not_create_alarm_for_normal_pressure(
        decimal value)
    {
        // Arrange
        var reading = CreateReading(
            SensorType.Pressure,
            value,
            "bar");

        // Act
        var alarm = _evaluator.Evaluate(reading);

        // Assert
        Assert.Null(alarm);
    }

    private static SensorReading CreateReading(
        SensorType type,
        decimal value,
        string unit)
    {
        return new SensorReading(
            Guid.NewGuid(),
            Guid.NewGuid(),
            type,
            value,
            unit,
            DateTimeOffset.UtcNow);
    }
}