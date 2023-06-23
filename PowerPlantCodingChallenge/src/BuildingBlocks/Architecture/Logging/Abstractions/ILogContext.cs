namespace Engie.PowerPlantCodingChallenge.BuildingBlocks.Architecture.Logging.Abstractions;

public interface ILogContext
{
    IDisposable AddProperty( string name, object value );
}
