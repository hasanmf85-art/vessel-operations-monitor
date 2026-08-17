using VesselOperationsMonitor.Application;
using VesselOperationsMonitor.Domain;
using VesselOperationsMonitor.Simulator;

IReadOnlyList<VesselSetup> vesselSetups = DemoData.CreateVessels();
var simulator = new SensorSimulator(vesselSetups);

var alarmEvaluator = new AlarmEvaluator();

var alarmCount = 0;

using var cancellationTokenSource = new CancellationTokenSource();

Console.CancelKeyPress += (_, eventArgs) =>
{
    eventArgs.Cancel = true;
    cancellationTokenSource.Cancel();
};

Console.WriteLine("VESSEL OPERATIONS MONITOR");
Console.WriteLine("=========================");
Console.WriteLine();
Console.WriteLine("Simulator is running. Press Ctrl+C to stop.");
Console.WriteLine();

try
{
    await foreach (var reading in simulator.GeneratingReadingsAsync(
                       TimeSpan.FromSeconds(3),
                       cancellationTokenSource.Token))
    {
        var setup = vesselSetups.Single(
            item => item.Vessel.Id == reading.VesselId);

        var sensor = setup.Sensors.Single(
            item => item.Id == reading.SensorId);

        setup.Vessel.RegisterContact(reading.RecordedAt);

        Console.WriteLine(
            $"{reading.RecordedAt:HH:mm:ss} | " +
            $"{setup.Vessel.Name,-18} | " +
            $"{sensor.Name,-28} | " +
            $"{reading.Value,6:F1} {reading.Unit}");

        var alarm = alarmEvaluator.Evaluate(reading);

        if (alarm != null)
        {
            alarmCount++;
            
            var previousColor  =  Console.ForegroundColor;

            Console.ForegroundColor = alarm.Severity switch
            {
                AlarmSeverity.Critical => ConsoleColor.Red,
                AlarmSeverity.Warning => ConsoleColor.Yellow,
                _ => ConsoleColor.DarkGreen
            };

            Console.WriteLine(
                $" Alarm [{alarm.Severity}] " +
                $" - {alarm.Message} " +
                $" - Alarmed Value: {alarm.TriggerValue:F1} {reading.Unit}"
            );

            Console.ForegroundColor = previousColor;

        }


    }
}
catch (OperationCanceledException)
{
    Console.WriteLine();
    Console.WriteLine("Simulator stopped.");
}