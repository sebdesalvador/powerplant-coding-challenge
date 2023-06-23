namespace Engie.PowerPlantCodingChallenge.Presentation.Api.Controllers;

/// <summary>
/// Controller for the <see cref="PowerPlantToBeActivated" /> resource
/// </summary>
[ ApiController ]
[ Route( "[controller]" ) ]
[ Produces( MediaTypeNames.Application.Json ) ]
public class PowerPlantsController : ControllerBase
{
    private readonly ILogger< PowerPlantsController > _logger;
    private readonly IMediator _mediator;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="mediator"></param>
    public PowerPlantsController( ILogger< PowerPlantsController > logger, IMediator mediator )
    {
        _logger = logger;
        _mediator = mediator;
    }

    /// <summary>
    /// Get the list of power plants to be activated
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [ HttpPost( "merit-order" ) ]
    [ ProducesResponseType( typeof( IEnumerable< PowerPlantToBeActivated > ), StatusCodes.Status200OK ) ]
    [ ProducesResponseType( typeof( string ), StatusCodes.Status400BadRequest, MediaTypeNames.Text.Plain ) ]
    [ ProducesResponseType( typeof( string ), StatusCodes.Status500InternalServerError, MediaTypeNames.Text.Plain ) ]
    public async Task< IActionResult > GetList( [ FromBody ] GetListOfPowerPlantsToActivateQuery query,
                                                CancellationToken cancellationToken = default )
    {
        var retCol = await _mediator.Send( query, cancellationToken );

        if ( retCol == null )
            return new ContentResult
            {
                Content = "Something went wrong.",
                ContentType = MediaTypeNames.Text.Plain,
                StatusCode = StatusCodes.Status500InternalServerError
            };

        if ( !retCol.Any() )
            return BadRequest();

        return Ok( retCol );
    }
}
