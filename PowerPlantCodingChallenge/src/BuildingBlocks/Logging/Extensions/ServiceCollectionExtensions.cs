namespace Engie.PowerPlantCodingChallenge.BuildingBlocks.Logging.Extensions;

[ ExcludeFromCodeCoverage ]
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLogContext( this IServiceCollection services )
        => services.AddSingleton< ILogContext, LogContext >();

    public static IServiceCollection AddDefaultLogging( this IServiceCollection services, string applicationName )
        => services
          .AddLogContext()
          .AddLogging( logging =>
           {
               var config = new LoggerConfiguration().MinimumLevel.Warning()
                                                     .MinimumLevel.Override( "Azure.Core", LogEventLevel.Error )
                                                     .MinimumLevel.Override( "Azure.Messaging.EventHubs", LogEventLevel.Error )
                                                     .MinimumLevel.Override( applicationName, LogEventLevel.Information )
                                                     .Enrich.FromLogContext()
                                                     .Enrich.WithProperty( "EnvironmentName", Environment.GetEnvironmentVariable( "AZURE_FUNCTIONS_ENVIRONMENT" ) ?? "NoEnvironment" )
                                                     .Enrich.WithProperty( "ApplicationName", applicationName )
                                                     .WriteTo.AzureAnalytics( Environment.GetEnvironmentVariable( "AzureAnalyticsWorkspaceId" ),
                                                                              Environment.GetEnvironmentVariable( "AzureAnalyticsAuthenticationId" ),
                                                                              Environment.GetEnvironmentVariable( "AzureAnalyticsLogName" ) )
                                                     .WriteTo.File( "..\\..\\LogFiles\\AppLog_.log",
                                                                    rollingInterval: RollingInterval.Day,
                                                                    rollOnFileSizeLimit: true,
                                                                    retainedFileCountLimit: 3,
                                                                    shared: true,
                                                                    flushToDiskInterval: TimeSpan.FromSeconds( 1 ) );
               config.ApplyAzureAnalyticsSinkSerializerWorkaround();
               var logger = config.CreateLogger();
               logging.AddSerilog( logger, dispose: true );
           } );
}
