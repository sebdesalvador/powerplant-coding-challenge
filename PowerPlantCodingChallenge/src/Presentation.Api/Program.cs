try
{
    var builder = WebApplication.CreateBuilder( args );
    builder.Host.UseSerilogWithHostBuilderLogger( ( context, log ) =>
    {
        log.ReadFrom.Configuration( context.Configuration );
        log.ApplyAzureAnalyticsSinkSerializerWorkaround();
    } );

    builder.Services.Configure< RouteOptions >( options => options.LowercaseUrls = true );
    builder.Services.AddLogContext();
    builder.Services
           .AddControllers()
           .AddNewtonsoftJson( options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                options.SerializerSettings.ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                };
            } );
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen( options =>
    {
        options.SwaggerDoc( "v1", new OpenApiInfo
        {
            Title = "Engie Power Plant Coding Challenge API",
            Description = "",
            Version = "v0.0.1"
        } );
        options.IncludeXmlComments( Path.Combine( AppContext.BaseDirectory, "EngiePowerPlantCodingChallengeApi.xml" ) );
        options.UseAllOfForInheritance();
        options.UseOneOfForPolymorphism();
        options.SelectSubTypesUsing( baseType =>
        {
            if ( baseType.Assembly.FullName != null
              && baseType.Assembly.FullName.StartsWith( "Engie.PowerPlantCodingChallenge.Core" ) )
                return baseType.Assembly
                               .GetTypes()
                               .Where( type => type.IsSubclassOf( baseType ) );

            return typeof( Program ).Assembly
                                    .GetTypes()
                                    .Where( type => type.IsSubclassOf( baseType ) );
        } );
        options.CustomOperationIds( ad =>
        {
            if ( !ad.TryGetMethodInfo( out var methodInfo ) )
                throw new Exception( "Unable to get method info to generate operation ID." );

            var letters = methodInfo.Name.ToCharArray();
            letters[ 0 ] = letters[ 0 ].ToString().ToLowerInvariant().ToCharArray()[ 0 ];
            return string.Join( "", letters );
        } );
    } );
    builder.Services.AddResponseCompression( options => options.Providers.Add< BrotliCompressionProvider >() );
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure();

    var app = builder.Build();
    app.UseGlobalExceptionHandling();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpsRedirection();
    app.UseResponseCompression();
    app.MapControllers();
    app.Run();
}
catch ( Exception e )
{
    HostBuilderLogger.Logger.LogError( "Program.Main caught exception", e );
    await HostBuilderLogger.Logger.TerminalEmitCachedMessages( args );
}
