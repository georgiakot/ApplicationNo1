namespace ApplicationNo1.Users.Vehicles
{
    public class Car : VehicleBase
    {
        #region Constructor
        public Car()
        {
            _maxFuel = 50;
            _factor = 0.08;
        }
        #endregion

        #region Methods

        public override bool Drive(double distance)
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
    }
}
