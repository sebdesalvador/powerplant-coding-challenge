namespace Engie.PowerPlantCodingChallenge.Core.Domain.Model.PowerPlantAggregate;

public class WindTurbine : PowerPlant
{
    public WindTurbine( string name, double maxPower, double windSpeed )
        : base( name, maxPower )
    {
        if ( windSpeed < 0 )
            throw new PowerPlantException( PowerPlantExceptionType.WindSpeedMustBePositive );

        MaxPower *= windSpeed / 100;
        Efficiency = 1;
        Cost = 0;
    }
}
