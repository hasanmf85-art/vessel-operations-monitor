using VesselOperationsMonitor.Domain;

namespace VesselOperationsMonitor.Simulator;

public static class DemoData
{
    public static IReadOnlyList<VesselSetup> CreateVessels()
    {
        var ocean = CreateVesselSetup(
            "Høglund Ocean",
            "1234567");

        var explorer = CreateVesselSetup(
            "Høglund Explorer",
            "2345678");

        var voyager = CreateVesselSetup(
            "Høglund Voyager",
            "3456789");

        return [ocean, explorer, voyager];
    }

    private static VesselSetup CreateVesselSetup(
        string vesselName,
        string imoNumber)
    {
        var vessel = new Vessel(
            vesselName,
            imoNumber);

        var sensors = new List<Sensor>
        {
            new(
                vessel.Id,
                "Main Engine Temperature",
                SensorType.Temperature,
                "°C"),

            new(
                vessel.Id,
                "Main Engine Oil Pressure",
                SensorType.Pressure,
                "bar")
        };

        return new VesselSetup(
            vessel,
            sensors);
    }
}