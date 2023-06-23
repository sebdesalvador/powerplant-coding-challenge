namespace Engie.PowerPlantCodingChallenge.Application.Queries;

[ ExcludeFromCodeCoverage ]
public class GetListOfPowerPlantsToActivateQuery : IRequest< PowerPlantToBeActivated[] >
{
    public double Load { get; set; }
    public Variables Fuels { get; set; }

    [ JsonProperty( "powerplants" ) ]
    public PowerPlant[] PowerPlants { get; set; }

    public class Variables
    {
        [ JsonProperty( "gas(euro/MWh)" ) ]
        public double Gas { get; set; }

        [ JsonProperty( "kerosine(euro/MWh)" ) ]
        public double Kerosine { get; set; }

        [ JsonProperty( "co2(euro/ton)" ) ]
        public double Co2 { get; set; }

        [ JsonProperty( "wind(%)" ) ]
        public double Wind { get; set; }
    }

    public class PowerPlant
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public double Efficiency { get; set; }

        [ JsonProperty( "pmin" ) ]
        public double MinPower { get; set; }

        [ JsonProperty( "pmax" ) ]
        public double MaxPower { get; set; }
    }
}
