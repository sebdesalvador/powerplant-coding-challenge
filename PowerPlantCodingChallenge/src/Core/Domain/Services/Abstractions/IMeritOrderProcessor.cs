namespace Engie.PowerPlantCodingChallenge.Core.Domain.Services.Abstractions;

public interface IMeritOrderProcessor
{
    IEnumerable< PowerPlantToBeActivated > Process( double load,
                                                    double gasPricePerMegaWattHour,
                                                    double kerosinePerMegaWattHour,
                                                    List< PowerPlant > powerPlants );
}
