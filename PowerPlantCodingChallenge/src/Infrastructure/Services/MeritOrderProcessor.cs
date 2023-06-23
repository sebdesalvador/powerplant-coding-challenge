namespace Engie.PowerPlantCodingChallenge.Infrastructure.Services;

public class MeritOrderProcessor : IMeritOrderProcessor
{
    public IEnumerable< PowerPlantToBeActivated > Process( double load,
                                                           double gasPricePerMegaWattHour,
                                                           double kerosinePerMegaWattHour,
                                                           List< PowerPlant > powerPlants )
    {
        powerPlants.Sort( ( x, y ) => x.Cost.CompareTo( y.Cost ) );
        var powerPlantsToBeActivated = powerPlants.Select( plant => new PowerPlantToBeActivated { Name = plant.Name, P = 0 } ).ToList();

        for ( var i = 0; i < powerPlants.Count; i++ )
        {
            if ( load <= 0 )
                break;

            if ( powerPlants[ i ] is GasFired gasFired && load < gasFired.MinPower )
                continue;

            var allocatedP = Math.Min( load, powerPlants[ i ].MaxPower );
            powerPlantsToBeActivated[ i ].P = allocatedP;
            load -= allocatedP;
        }

        return powerPlantsToBeActivated;
    }
}
