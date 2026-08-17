using System.Runtime.CompilerServices;
using VesselOperationsMonitor.Domain;

namespace VesselOperationsMonitor.Simulator;

public class SensorSimulator
{
    private readonly IReadOnlyList<VesselSetup> _vesselSetups;

    public SensorSimulator(IReadOnlyList<VesselSetup> vesselSetups)
    {
        _vesselSetups = vesselSetups;
    }
    
    public async IAsyncEnumerable<SensorReading> GeneratingReadingsAsync(
        TimeSpan interval, [EnumeratorCancellation] CancellationToken cancellationToken = default
        )
    {
        using var timer = new PeriodicTimer(interval);

        while (await timer.WaitForNextTickAsync(cancellationToken))
        {

            foreach (var setup in _vesselSetups)
            {
                foreach (var sensor in setup.Sensors)
                {
                    if (!sensor.IsActive)
                    {
                        continue;
                    }
                    
                    var value = GenerateValue(sensor.Type);
                    
                    yield return new SensorReading(
                        setup.Vessel.Id,
                        sensor.Id,
                        sensor.Type,
                        value,
                        sensor.Unit,
                        DateTimeOffset.UtcNow);
                }
            }
        }
    }    
    
    private static decimal GenerateValue(SensorType sensorType)
    {
        return sensorType switch
        {
            SensorType.Temperature =>
                Random.Shared.Next(650, 1160) / 10m,

            SensorType.Pressure =>
                Random.Shared.Next(10, 81) / 10m,

            _ => throw new ArgumentOutOfRangeException(
                nameof(sensorType),
                sensorType,
                "Unsupported sensor type.")
        };
    }
}