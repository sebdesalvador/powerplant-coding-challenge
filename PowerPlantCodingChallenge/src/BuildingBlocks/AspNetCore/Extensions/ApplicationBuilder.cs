namespace Engie.PowerPlantCodingChallenge.BuildingBlocks.AspNetCore.Extensions;

[ ExcludeFromCodeCoverage ]
[ SuppressMessage( "ReSharper", "UnusedType.Global" ) ]
public static class ApplicationBuilderExtensions
{
    private const string GLOBAL_EXCEPTION_HANDLING_ADDED = "GlobalExceptionHandlingAdded";

    [ SuppressMessage( "ReSharper", "UnusedMember.Global" ) ]
    public static IApplicationBuilder UseGlobalExceptionHandling( this IApplicationBuilder builder )
    {
        if ( builder == null ) throw new ArgumentNullException( nameof( builder ) );
        if ( builder.Properties.ContainsKey( GLOBAL_EXCEPTION_HANDLING_ADDED ) ) return builder;

        builder.Properties[ GLOBAL_EXCEPTION_HANDLING_ADDED ] = true;
        return builder.UseMiddleware< GlobalExceptionHandlerMiddleware >();
    }
}
