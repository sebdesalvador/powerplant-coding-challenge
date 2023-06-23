namespace Engie.PowerPlantCodingChallenge.Core.Domain.Model.PowerPlantAggregate;

public class PowerPlantException : DomainException
{
    public PowerPlantException( PowerPlantExceptionType type, Exception? innerException = null )
        : base( type, innerException ) { }
}
