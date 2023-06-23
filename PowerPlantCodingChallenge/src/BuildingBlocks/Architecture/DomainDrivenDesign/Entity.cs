namespace Engie.PowerPlantCodingChallenge.BuildingBlocks.Architecture.DomainDrivenDesign;

[ ExcludeFromCodeCoverage ]
public abstract class Entity< T > : IEquatable< Entity< T > >
    where T : Identity, new()
{
    #region Fields

    private int? _requestedHashCode;
    private readonly List< DomainEvent > _domainEvents = new();

    #endregion

    #region Properties

    public virtual T Id { get; } = new();
    public virtual DateTime CreatedOn { get; set; }
    public virtual DateTime LastModifiedOn { get; set; }
    public DateTime? DeletedOn { get; set; }
    public IReadOnlyCollection< DomainEvent > DomainEvents => _domainEvents.AsReadOnly();

    #endregion

    #region Constructors

    protected Entity() { }

    protected Entity( T identity ) : this() => Id = identity;

    protected Entity( Guid identityValue ) : this() => InitializeIdentity( identityValue );

    #endregion

    #region Interfaces Implementation

    public bool Equals( Entity< T > other )
    {
        if ( ReferenceEquals( this, other ) )
            return true;

        if ( other == null || other.GetType() != GetType() )
            return false;

        return other.Id == Id;
    }

    public void ClearDomainEvents() => _domainEvents.Clear();

    #endregion

    #region Overrides

    public override bool Equals( object other ) => Equals( other as Entity< T > );

    public override int GetHashCode()
    {
        _requestedHashCode ??= Id.GetHashCode() ^ 31;
        return _requestedHashCode.Value;
    }

    #endregion

    #region Operators

    public static bool operator ==( Entity< T > a, Entity< T > b ) => a?.Equals( b ) ?? Equals( b, null );
    public static bool operator !=( Entity< T > a, Entity< T > b ) => !( a == b );

    #endregion

    #region Public Methods

    protected void AddDomainEvent( DomainEvent @event ) => _domainEvents.Add( @event );

    #endregion

    #region Private Methods

    private void InitializeIdentity( Guid identityValue ) => Id.Initialize( identityValue );

    #endregion
}
