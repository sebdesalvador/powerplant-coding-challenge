namespace Engie.PowerPlantCodingChallenge.Core.Domain.Model.PowerPlantAggregate;

public class GasFired : PowerPlant
{
    private readonly double _minPower;

    public double MinPower
    {
        get => _minPower;
        private init
        {
            if ( value < 0 )
                throw new PowerPlantException( PowerPlantExceptionType.MinPowerMustBePositive );

            if ( value > MaxPower )
                throw new PowerPlantException( PowerPlantExceptionType.MinPowerMustBeLessThanMaxPower );

            _minPower = value;
        }
    }

    public GasFired( string name, double maxPower, double minPower, double efficiency, double pricePerMegaWattHour )
        : base( name, maxPower )
    {
        MinPower = minPower;
        Efficiency = efficiency;
        Cost = pricePerMegaWattHour / efficiency;
    }
}
