namespace Engie.PowerPlantCodingChallenge.BuildingBlocks.AspNetCore.Middleware;

[ ExcludeFromCodeCoverage ]
public class GlobalExceptionHandlerMiddleware
{
    private readonly ILogger< GlobalExceptionHandlerMiddleware > _logger;
    private readonly RequestDelegate _next;

    public GlobalExceptionHandlerMiddleware( ILogger< GlobalExceptionHandlerMiddleware > logger,
                                             RequestDelegate next )
    {
        _logger = logger;
        _next = next;
    }

    public async Task Invoke( HttpContext httpContext )
    {
        try
        {
            await _next( httpContext );
        }
        catch ( Exception e )
        {
            httpContext.Response.StatusCode = e switch
            {
                KeyNotFoundException => ( int )HttpStatusCode.NotFound,
                _ => ( int )HttpStatusCode.InternalServerError
            };
            _logger.LogError( e, "Unhandled exception" );
        }
    }
}
