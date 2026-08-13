namespace VesselOperationsMonitor.Domain;

public class SoftwareInstallation
{
    public Guid  Id { get; private set; }
    public Guid VesselId { get; }
    
    public string Version { get;}
    public DateTimeOffset InstallDate { get; }

    public SoftwareInstallation( Guid vesselId, string version, DateTimeOffset installDate)
    {
        if (vesselId == Guid.Empty) throw new ArgumentException("vesselId cannot be empty", nameof(vesselId));
        
        if (string.IsNullOrEmpty(version)) throw new ArgumentException("version cannot be empty", nameof(version));
        
        if (installDate > DateTimeOffset.UtcNow) throw new ArgumentException("installDate cannot be in the future", nameof(installDate));
        
        Id = Guid.NewGuid();
        VesselId = vesselId;
        Version = version;  
        InstallDate = installDate;
    }
    
    
}