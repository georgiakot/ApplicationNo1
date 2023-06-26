namespace ApplicationNo1.Users.Vehicles
{
    public class Bus : VehicleBase
    {
        #region Constructor
        public Bus()
        {
            _maxFuel = 450;
            _factor = 1.1;
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
