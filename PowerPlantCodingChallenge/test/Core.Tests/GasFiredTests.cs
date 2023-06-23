namespace Engie.PowerPlantCodingChallenge.Core.Tests;

public class GasFiredTests
{
    [ Fact ]
    public void GasFired_CanBeCreated()
    {
        var gasFired = new GasFired( " gasfired1 ", 460, 100, .5, 10 );
        Assert.Equal( "gasfired1", gasFired.Name );
        Assert.Equal( .5, gasFired.Efficiency );
        Assert.Equal( 460, gasFired.MaxPower );
        Assert.Equal( 100, gasFired.MinPower );
        Assert.Equal( 20, gasFired.Cost );
    }

    [ Theory ]
    [ InlineData( -10 ) ]
    [ InlineData( 500 ) ]
    public void GasFired_WhenMinPowerIsInvalid_ThrowsPowerPlantException( double minPower )
        => Assert.Throws< PowerPlantException >( () => new GasFired( "gasfired1", 460, minPower, 0.5, 0 ) );
}
