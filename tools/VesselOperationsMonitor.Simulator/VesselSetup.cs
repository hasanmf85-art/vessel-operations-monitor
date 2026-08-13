using VesselOperationsMonitor.Domain;

namespace VesselOperationsMonitor.Simulator;

public sealed record VesselSetup(
    Vessel Vessel,
    IReadOnlyList<Sensor> Sensors);