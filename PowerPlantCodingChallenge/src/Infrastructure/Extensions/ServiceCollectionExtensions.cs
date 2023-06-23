namespace Engie.PowerPlantCodingChallenge.Infrastructure.Extensions;

[ ExcludeFromCodeCoverage ]
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure( this IServiceCollection services )
        => services.AddSingleton< IMeritOrderProcessor, MeritOrderProcessor >();
}
