namespace Engie.PowerPlantCodingChallenge.BuildingBlocks.Logging;

[ ExcludeFromCodeCoverage ]
public class LogContext : ILogContext
{
    public IDisposable AddProperty( string name, object value )
        => Serilog.Context.LogContext.PushProperty( name, value );
}
