namespace Engie.PowerPlantCodingChallenge.Core.Domain.Model.PowerPlantAggregate;

public class PowerPlantExceptionType : Enumeration
{
    public static readonly PowerPlantExceptionType PowerPlantNameEmpty = new( Guid.Parse( "00000000-0000-0000-0000-000000000001" ), "POWER_PLANT_NAME_EMPTY" );
    public static readonly PowerPlantExceptionType PowerPlantNameTooLong = new( Guid.Parse( "00000000-0000-0000-0000-000000000002" ), "POWER_PLANT_NAME_TOO_LONG" );
    public static readonly PowerPlantExceptionType EfficiencyMustBePositive = new( Guid.Parse( "00000000-0000-0000-0000-000000000003" ), "EFFICIENCY_MUST_BE_POSITIVE" );
    public static readonly PowerPlantExceptionType EfficiencyMustBeLessThanOne = new( Guid.Parse( "00000000-0000-0000-0000-000000000004" ), "EFFICIENCY_MUST_BE_LESS_THAN_ONE" );
    public static readonly PowerPlantExceptionType MaxPowerMustBePositive = new( Guid.Parse( "00000000-0000-0000-0000-000000000005" ), "MAX_POWER_MUST_BE_POSITIVE" );
    public static readonly PowerPlantExceptionType MinPowerMustBePositive = new( Guid.Parse( "00000000-0000-0000-0000-000000000006" ), "MIN_POWER_MUST_BE_POSITIVE" );
    public static readonly PowerPlantExceptionType MinPowerMustBeLessThanMaxPower = new( Guid.Parse( "00000000-0000-0000-0000-000000000007" ), "MIN_POWER_MUST_BE_LESS_THAN_MAX_POWER" );
    public static readonly PowerPlantExceptionType WindSpeedMustBePositive = new( Guid.Parse( "00000000-0000-0000-0000-000000000008" ), "WIND_SPEED_MUST_BE_POSITIVE" );
    public static readonly PowerPlantExceptionType UnknownPowerPlantType = new( Guid.Parse( "00000000-0000-0000-0000-000000000009" ), "UNKNOWN_POWER_PLANT_TYPE" );

    private PowerPlantExceptionType( Guid id, string name )
        : base( id, name ) { }
}
