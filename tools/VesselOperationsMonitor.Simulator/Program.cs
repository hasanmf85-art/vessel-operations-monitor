using VesselOperationsMonitor.Simulator;

var vesselSetups = DemoData.CreateVessels();

Console.WriteLine("VESSEL OPERATIONS MONITOR");
Console.WriteLine("========================");
Console.WriteLine();

foreach (var setup in vesselSetups)
{
    Console.WriteLine(
        $"Vessel: {setup.Vessel.Name} " +
        $"(IMO {setup.Vessel.ImoNumber})");

    Console.WriteLine(
        $"Vessel ID: {setup.Vessel.Id}");

    foreach (var sensor in setup.Sensors)
    {
        Console.WriteLine(
            $"  Sensor: {sensor.Name} | " +
            $"{sensor.Type} | " +
            $"{sensor.Unit}");

        Console.WriteLine(
            $"  Sensor ID: {sensor.Id}");

        Console.WriteLine(
            $"  Connected Vessel ID: {sensor.VesselId}");
    }

    Console.WriteLine();
}