using System.Diagnostics;
using VesselOperationsMonitor.Domain;

namespace VesselOperationsMonitor.Application;

public class AlarmEvaluator
{
    public Alarm? Evaluate(SensorReading reading)
    {
        var alarmRule = (reading.Type, reading.Value) switch
        {
            (SensorType.Temperature, >= 105m) => new AlarmRule(AlarmSeverity.Critical,
                "Engine temperature is critically high."),
            (SensorType.Temperature, >= 95m) => new AlarmRule(AlarmSeverity.Warning,
                "Engine temperature is high."),
            (SensorType.Pressure, <= 2m) => new AlarmRule(AlarmSeverity.Critical,
                "Engine oil pressure is critically low."),
            (SensorType.Pressure, <= 3m) => new AlarmRule(AlarmSeverity.Warning,
                "Engine oil pressure is low."),
            _=> null
        };
        
        if (alarmRule is null) return null;

        return new Alarm(
            reading.VesselId,
            reading.SensorId,
            alarmRule.Message,
            alarmRule.Severity,
            reading.Value,
            reading.RecordedAt
            );

    }

    private sealed record AlarmRule(
        AlarmSeverity Severity,
        string Message);
}