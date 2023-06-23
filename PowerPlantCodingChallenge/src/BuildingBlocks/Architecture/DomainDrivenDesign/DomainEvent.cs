namespace Engie.PowerPlantCodingChallenge.BuildingBlocks.Architecture.DomainDrivenDesign;

[ ExcludeFromCodeCoverage ]
[ Serializable ]
public abstract class DomainEvent : INotification
{
    public Guid Id { get; private set; }
    public Guid? CausationId { get; private set; }
    public Guid CorrelationId { get; private set; }
    public DateTime OccurredOn { get; private set; }
    public string TypeName { get; private set; }
    public string AssemblyQualifiedTypeName { get; private set; }

    protected DomainEvent()
    {
        Id = Guid.NewGuid();
        OccurredOn = DateTime.UtcNow;
        TypeName = GetType().Name;
        AssemblyQualifiedTypeName = GetType().AssemblyQualifiedName;
    }

    public void SetCorrelationIds( Guid? causationId, Guid correlationId )
    {
        CausationId = causationId;
        CorrelationId = correlationId;
    }
}
