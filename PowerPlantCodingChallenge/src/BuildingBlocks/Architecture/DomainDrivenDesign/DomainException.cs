namespace Engie.PowerPlantCodingChallenge.BuildingBlocks.Architecture.DomainDrivenDesign;

[ ExcludeFromCodeCoverage ]
public abstract class DomainException : Exception
{
    public Enumeration Type { get; }

    protected DomainException( Enumeration type, Exception? innerException = null )
        : base( $"[{type.Id}] {type.Name}", innerException )
        => Type = type;
}
