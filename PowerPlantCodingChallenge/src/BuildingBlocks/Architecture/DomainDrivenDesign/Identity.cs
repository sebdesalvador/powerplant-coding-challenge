namespace Engie.PowerPlantCodingChallenge.BuildingBlocks.Architecture.DomainDrivenDesign;

[ ExcludeFromCodeCoverage ]
public abstract class Identity : IEquatable< Identity >, IComparable< Identity >
{
    #region Properties

    public Guid Value { get; private set; }

    #endregion

    #region Constructor

    protected Identity() => Value = Guid.NewGuid();
    public Identity( Guid identityValue ) => Value = identityValue;

    #endregion

    #region Interfaces Implementation

    public void Initialize( object initialValue )
    {
        if ( initialValue is Guid initialGuidValue )
            Value = initialGuidValue;
    }

    public bool Equals( Identity other )
    {
        if ( ReferenceEquals( this, other ) )
            return true;

        if ( other == null || other.GetType() != GetType() )
            return false;

        return Value.Equals( other.Value );
    }

    public int CompareTo( Identity other ) => Value.CompareTo( other.Value );

    #endregion

    #region Overrides

    public override string ToString() => $"{GetType().Name} [Id={Value}]";
    public override bool Equals( object other ) => Equals( other as Identity );
    public override int GetHashCode() => Value.GetHashCode();

    #endregion

    #region Operators

    public static bool operator ==( Identity a, Identity b ) => a?.Equals( b ) ?? ReferenceEquals( b, null );
    public static bool operator !=( Identity a, Identity b ) => !( a == b );

    #endregion
}
