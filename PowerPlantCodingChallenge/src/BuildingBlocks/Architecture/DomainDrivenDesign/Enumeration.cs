namespace Engie.PowerPlantCodingChallenge.BuildingBlocks.Architecture.DomainDrivenDesign;

[ ExcludeFromCodeCoverage ]
public abstract class Enumeration : IEquatable< Enumeration >, IComparable< Enumeration >
{
    #region Properties

    public string Name { get; private set; }
    public Guid Id { get; private set; }

    #endregion

    #region Constructors

    protected Enumeration() { }
    protected Enumeration( Guid id, string name ) => ( Id, Name ) = ( id, name );

    #endregion

    #region Interfaces Implementation

    public bool Equals( Enumeration other )
    {
        if ( ReferenceEquals( this, other ) )
            return true;

        if ( other == null )
            return false;

        if ( other.GetType() != GetType() )
            return false;

        return Id.Equals( other.Id );
    }

    public int CompareTo( Enumeration other ) => Id.CompareTo( other.Id );

    #endregion

    #region Overrides

    public override string ToString() => Name;
    public override bool Equals( object other ) => Equals( other as Enumeration );
    public override int GetHashCode() => Id.GetHashCode();

    #endregion

    #region Public Methods

    public static IEnumerable< T > GetAll< T >() where T : Enumeration
    {
        var fields = typeof( T ).GetFields( BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly );
        return fields.Select( f => f.GetValue( null ) ).Cast< T >();
    }

    public static T FromValue< T >( Guid value ) where T : Enumeration
    {
        var matchingItem = Parse< T, Guid >( value, "value", item => item.Id == value );
        return matchingItem;
    }

    public static T FromDisplayName< T >( string displayName )
        where T : Enumeration
    {
        var matchingItem = Parse< T, string >( displayName, "display name", item => item.Name == displayName );
        return matchingItem;
    }

    #endregion

    #region Operators

    public static bool operator ==( Enumeration a, Enumeration b ) => a?.Equals( b ) ?? ReferenceEquals( b, null );
    public static bool operator !=( Enumeration a, Enumeration b ) => !( a == b );

    #endregion

    #region Private Methods

    private static T Parse< T, TK >( TK value, string description, Func< T, bool > predicate )
        where T : Enumeration
    {
        var matchingItem = GetAll< T >().FirstOrDefault( predicate );

        if ( matchingItem == null )
            throw new InvalidOperationException( $"'{value}' is not a valid {description} in {typeof( T )}" );

        return matchingItem;
    }

    #endregion
}
