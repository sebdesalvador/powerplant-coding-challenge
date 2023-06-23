namespace Engie.PowerPlantCodingChallenge.Core.Domain.Model.PowerPlantAggregate;

public abstract class PowerPlant : Entity< PowerPlantId >, IAggregateRoot
{
    private readonly string _name;
    private readonly double _efficiency;
    private readonly double _maxPower;

    public string Name
    {
        get => _name;
        private init
        {
            if ( string.IsNullOrWhiteSpace( value ) )
                throw new PowerPlantException( PowerPlantExceptionType.PowerPlantNameEmpty );

            var trimmedValue = value.Trim();

            if ( trimmedValue.Length > 255 )
                throw new PowerPlantException( PowerPlantExceptionType.PowerPlantNameTooLong );

            _name = trimmedValue;
        }
    }

    public double Efficiency
    {
        get => _efficiency;
        protected init
        {
            if ( value < 0 )
                throw new PowerPlantException( PowerPlantExceptionType.EfficiencyMustBePositive );

            if ( value > 1 )
                throw new PowerPlantException( PowerPlantExceptionType.EfficiencyMustBeLessThanOne );

            _efficiency = value;
        }
    }

    public double MaxPower
    {
        get => _maxPower;
        protected init
        {
            if ( value < 0 )
                throw new PowerPlantException( PowerPlantExceptionType.MaxPowerMustBePositive );

            _maxPower = value;
        }
    }

    public double Cost { get; protected set; }

    protected PowerPlant( string name, double maxPower )
    {
        Name = name;
        MaxPower = maxPower;
    }
}
