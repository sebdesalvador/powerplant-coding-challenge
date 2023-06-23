namespace Engie.PowerPlantCodingChallenge.Core.Domain.Model.PowerPlantAggregate;

public class TurboJet : PowerPlant
{
    public TurboJet( string name, double maxPower, double efficiency, double pricePerMegaWattHour )
        : base( name, maxPower )
    {
        Efficiency = efficiency;
        Cost = pricePerMegaWattHour / efficiency;
    }
}
