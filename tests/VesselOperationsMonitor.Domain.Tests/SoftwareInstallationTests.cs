namespace VesselOperationsMonitor.Domain.Tests;

public class SoftwareInstallationTests
{
    [Fact]
    public void should_create_installation()
    {
        var vesselId = Guid.NewGuid();
        var installDate = DateTimeOffset.UtcNow.AddMinutes(-5);
        var installation = new SoftwareInstallation(
            vesselId,
            "2.4.1",
            installDate);
        
        Assert.NotEqual(Guid.Empty, installation.Id);
        Assert.Equal(vesselId, installation.VesselId);
        Assert.Equal("2.4.1", installation.Version);
        Assert.Equal(installDate, installation.InstallDate);
    }
    
    [Fact]
    public void should_reject_empty_vessel_id()
    {
        var installDate = DateTimeOffset.UtcNow.AddMinutes(-5); 
        
        var exception = Assert.Throws<ArgumentException>(() => new SoftwareInstallation(Guid.Empty, "2.4.1", installDate));
        
        Assert.Equal("vesselId", exception.ParamName);
    }
    
    [Fact]
    public void should_reject_empty_version()
    {
        var vesselId = Guid.NewGuid();
        var installDate = DateTimeOffset.UtcNow.AddMinutes(-5); 
        
        var exception = Assert.Throws<ArgumentException>(() => new SoftwareInstallation(vesselId, "", installDate));
        
        Assert.Equal("version", exception.ParamName);
    }
    
    [Fact]
    public void should_reject_future_installation_date()
    {
        var vesselId = Guid.NewGuid();
        var installDate = DateTimeOffset.UtcNow.AddMinutes(55); 
        
        var exception = Assert.Throws<ArgumentException>(() => new SoftwareInstallation(vesselId, "2.4.2.1", installDate));
        
        Assert.Equal("installDate", exception.ParamName);  
        
        
    }

    
}