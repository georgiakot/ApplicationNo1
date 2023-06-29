namespace ApplicationNo1.Vehicle_
{
    public class VehicleBase : IVehicle
    {

        #region Fields

        protected double _maxFuel;
        protected double _factor;
        protected double _fuelLevel;
        protected double _refuel;
        protected double _kmCounter;

        #endregion

        #region Properties
        public double RefuelAmount { get { return _refuel; } set { _refuel = value; } }
        public double FuelLevel { get { return _fuelLevel; } set { _fuelLevel = value; } }
        public double MaxFuel { get { return _maxFuel; } set { _maxFuel = value; } }
        public double KmCounter { get { return _kmCounter; } set { _kmCounter = value; } }
        public string? Name { get; set; }
        public string? SearchTerm { get; set; }

        #endregion

        #region Constructor
        public VehicleBase()
        {
            _fuelLevel = 0;
            _kmCounter = 0;
        }
        #endregion

        #region Methods

        public RefuelResults Refuel(double money, double gasPrice)
        {
            if (SurpassesFuelLimit(money, gasPrice))
            {
                var moneyRequired = (_maxFuel - _fuelLevel) * gasPrice;

                _refuel = moneyRequired / gasPrice;
                _fuelLevel = _maxFuel;
                return RefuelResults.TooMuchMoneyNeedsChange;
            }
            else
            {
                _refuel = money / gasPrice;
                _fuelLevel += _refuel;
                return RefuelResults.NoChange;
            }
        }


        public virtual bool Drive(double distance)
        {
            if (SurpassesDistanceLimit(distance))
            {
                return false;
            }
            else
            {
                _fuelLevel -= distance * _factor;
                _kmCounter += distance;
                return true;
            }
        }

        #endregion

        #region Inputs & Validations

        private bool SurpassesFuelLimit(double money, double gasPrice)
        {
            return money / gasPrice > _maxFuel || _fuelLevel + money / gasPrice > _maxFuel;
        }

        public bool SurpassesDistanceLimit(double distance)
        {
            return _fuelLevel < distance * _factor || distance * _factor > _maxFuel;
        }

        #endregion

        #region Enums

        public enum RefuelResults
        {
            FuelOrderNotSatisfied,
            FuelOrderSatisfied
        }

        public enum DriveResults
        {
            Success,
            NotEnoughFuel
        }
        #endregion
    }
}
