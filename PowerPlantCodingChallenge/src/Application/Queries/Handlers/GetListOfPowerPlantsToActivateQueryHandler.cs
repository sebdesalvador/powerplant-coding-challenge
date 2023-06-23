using RequestPowerPlant = Engie.PowerPlantCodingChallenge.Application.Queries.GetListOfPowerPlantsToActivateQuery.PowerPlant;

namespace Engie.PowerPlantCodingChallenge.Application.Queries.Handlers;

public class GetListOfPowerPlantsToActivateQueryHandler
    : IRequestHandler< GetListOfPowerPlantsToActivateQuery, PowerPlantToBeActivated[] >
{
    private readonly ILogger< GetListOfPowerPlantsToActivateQueryHandler > _logger;
    private readonly IMeritOrderProcessor _meritOrderProcessor;

    public GetListOfPowerPlantsToActivateQueryHandler( ILogger< GetListOfPowerPlantsToActivateQueryHandler > logger,
                                                       IMeritOrderProcessor meritOrderProcessor )
    {
        _logger = logger;
        _meritOrderProcessor = meritOrderProcessor;
    }

    public Task< PowerPlantToBeActivated[] > Handle( GetListOfPowerPlantsToActivateQuery request,
                                                     CancellationToken cancellationToken )
    {
        try
        {
            var powerPlants = request.PowerPlants.Select< RequestPowerPlant, PowerPlant >( p => p.Type switch
            {
                "gasfired" => new GasFired( p.Name, p.MaxPower, p.MinPower, p.Efficiency, request.Fuels.Gas ),
                "turbojet" => new TurboJet( p.Name, p.MaxPower, p.Efficiency, request.Fuels.Kerosine ),
                "windturbine" => new WindTurbine( p.Name, p.MaxPower, request.Fuels.Wind ),
                _ => throw new PowerPlantException( PowerPlantExceptionType.UnknownPowerPlantType )
            } ).ToList();
            var powerPlantsToBeActivated = _meritOrderProcessor.Process( request.Load,
                                                                         request.Fuels.Gas,
                                                                         request.Fuels.Kerosine,
                                                                         powerPlants );
            return Task.FromResult( powerPlantsToBeActivated.ToArray() );
        }
        catch ( PowerPlantException e )
        {
            _logger.LogError( e, "Error while parsing request" );
            return Task.FromResult( Array.Empty< PowerPlantToBeActivated >() );
        }
        catch ( Exception e )
        {
            _logger.LogError( e, "Unknown error while calculating the unit-commitment" );
            return null;
        }
    }
}
