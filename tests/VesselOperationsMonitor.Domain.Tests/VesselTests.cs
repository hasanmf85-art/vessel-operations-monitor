namespace VesselOperationsMonitor.Domain.Tests;

public class VesselTests
{
    [Fact]
    public void should_register_vessel_with_uknown_status()
    {
        var vessel = new Vessel("Høglund Occean", "1234567");

        Assert.NotEqual(Guid.Empty, vessel.Id);
        Assert.Equal("Høglund Occean", vessel.Name);
        Assert.Equal("1234567", vessel.ImoNumber);
        Assert.Equal(VesselStatus.Unknown, vessel.Status);
        Assert.Null(vessel.LastContactAt);
    }

    [Fact]
    public void should_register_contact_with_marking_vessel_online()
    {
        var vessel = new Vessel("Høglund Occean", "1234567");

        var contactAt = DateTimeOffset.UtcNow;
        vessel.RegisterContact(contactAt);
        Assert.Equal(contactAt, vessel.LastContactAt);
        Assert.Equal(VesselStatus.Online, vessel.Status);
    }

    [Fact]
    public void should_reject_contact_older_previous_contact()
    {
        var vessel = new Vessel("Høglund Occean", "1234567");
        var newContact = DateTimeOffset.UtcNow;
        var oldercontact = newContact.AddMinutes(-5);

        vessel.RegisterContact(newContact);

        var exception = Assert.Throws<InvalidOperationException>(() => vessel.RegisterContact(oldercontact));

        Assert.Equal("Contact timestamp cannot be older than the previous contact.", exception.Message);
        Assert.Equal(newContact, vessel.LastContactAt);
    }

    [Fact]
    public void Should_be_stale_when_vessel_has_never_contacted()
    {
        var vessel = new Vessel("Høglund Occean", "1234567");
        var isStale = vessel.IsStale(DateTimeOffset.UtcNow, TimeSpan.FromMinutes(5));

        Assert.False(isStale);
    }

    [Fact]
    public void Should_be_stale_when_last_contact_exceeds_threshold()
    {
        var vessel = new Vessel("Høglund Occean", "1234567");
        var newContact = DateTimeOffset.UtcNow;

        vessel.RegisterContact(newContact);

        var now = newContact.AddMinutes(6);
        var threshold = TimeSpan.FromMinutes(5);
        var isVesselStale = vessel.IsStale(now, threshold);

        Assert.True(isVesselStale);
    }

    [Fact]
    public void should_reject_invalid_imoNumber()
    {
        var exception = Assert.Throws<ArgumentException>(() => new Vessel("Høglund Occean", "test"));
        Assert.Equal("imoNumber", exception.ParamName);
    }
}