namespace Engie.PowerPlantCodingChallenge.Core.Tests;

public class WindTurbineTests
{
    [ Fact ]
    public void WindTurbine_CanBeCreated()
    {
        var windTurbine = new WindTurbine( " windturbine1 ", 460, 100 );
        Assert.Equal( "windturbine1", windTurbine.Name );
        Assert.Equal( 460, windTurbine.MaxPower );
        Assert.Equal( 0, windTurbine.Cost );
    }

    [ Fact ]
    public void WindTurbine_WhenMinPowerIsNegative_ThrowsPowerPlantException()
        => Assert.Throws< PowerPlantException >( () => new WindTurbine( "windturbine1", 100, -10 ) );
}
