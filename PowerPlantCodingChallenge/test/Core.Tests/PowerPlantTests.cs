namespace Engie.PowerPlantCodingChallenge.Core.Tests;

public class PowerPlantTests
{
    [ Fact ]
    public void PowerPlant_CanBeCreated()
    {
        var turboJet = new TurboJet( " turbojet1 ", 460, .5, 100 );
        Assert.Equal( "turbojet1", turboJet.Name );
        Assert.Equal( .5, turboJet.Efficiency );
        Assert.Equal( 460, turboJet.MaxPower );
        Assert.Equal( 200, turboJet.Cost );
    }

    [ Theory ]
    [ InlineData( null ) ]
    [ InlineData( "" ) ]
    [ InlineData( "   " ) ]
    [ InlineData( "some very long name!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!" ) ]
    public void PowerPlant_WhenNameIsInvalid_ThrowsPowerPlantException( string name )
        => Assert.Throws< PowerPlantException >( () => new TurboJet( name, 0, 0, 0 ) );

    [ Theory ]
    [ InlineData( -1 ) ]
    [ InlineData( 1.1 ) ]
    public void PowerPlant_WhenEfficiencyIsInvalid_ThrowsPowerPlantException( double efficiency )
        => Assert.Throws< PowerPlantException >( () => new TurboJet( "turbojet1", 0, efficiency, 0 ) );

    [ Fact ]
    public void PowerPlant_WhenMaxPowerIsInvalid_ThrowsPowerPlantException()
        => Assert.Throws< PowerPlantException >( () => new TurboJet( "turbojet1", -1, 0, 0 ) );
}
