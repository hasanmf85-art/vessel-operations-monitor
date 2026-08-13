namespace VesselOperationsMonitor.Domain;

public sealed class Vessel
{
    public Guid Id { get; init; }
    public string Name { get;}
    public string ImoNumber { get; }
    public VesselStatus Status { get; private set; }
    public DateTimeOffset? LastContactAt { get; private set; }

    public Vessel(string name, string imoNumber)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required", nameof(name));
        if (string.IsNullOrWhiteSpace(imoNumber) || imoNumber.Length != 7 || !imoNumber.All(char.IsDigit)) throw new ArgumentException("ImoNumber is required", nameof(imoNumber));
        Id = Guid.NewGuid();
        Name = name;
        ImoNumber = imoNumber;
        Status = VesselStatus.Unknown;
        
    }

    public void RegisterContact(DateTimeOffset timestamp)
    {
        if (timestamp < LastContactAt)
            throw new InvalidOperationException(
                "Contact timestamp cannot be older than the previous contact.");

        LastContactAt = timestamp;
        Status = VesselStatus.Online;
    }

    public bool IsStale(DateTimeOffset now, TimeSpan threshold)
        => now - LastContactAt > threshold;

    private static bool isValidImoNumber(string? imoNumber)
    {
        if (imoNumber is not null && imoNumber.Length == 7 && imoNumber.All(char.IsDigit)) return true;
        return false;
    }
}