using System.Runtime.CompilerServices;
using VesselOperationsMonitor.Domain;

namespace VesselOperationsMonitor.Simulator;

public class SensorSimulator
{
    private readonly IReadOnlyList<Vessel> _vesselSetups;

    public SensorSimulator(IReadOnlyList<Vessel> vesselSetups)
    {
        _vesselSetups = vesselSetups;
    }
    
    public async IAsyncEnumerable<SensorReading> GeneratingReadingsAsync(
        TimeSpan interval, [EnumeratorCancellation] CancellationToken cancellationToken = default
        )
    {
        using var timer = new PeriodicTimer(interval):

        while (await timer.WaitForNextTickAsync(cancellationToken))
        {
            foreach (var vessel in _vesselSetups)
            {
                foreach (var sensor in setup.sensor)
                {
                    if (!sensor.isActive)
                    {
                        continue;
                    }
                    
                    var value = GenerateValue(sensor.Type);
                    
                    yield return new SensorReading(
                        setup.Vessel.Id,
                        sensor.Id
                        sensor.Type,
                            value,
                                );
                }
            }
        }
    }       
    
    
}