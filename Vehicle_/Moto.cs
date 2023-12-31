﻿using static ApplicationNo1.Vehicle_.VehicleBase;

namespace ApplicationNo1.Vehicle_
{
    public class Moto : VehicleBase
    {
        #region Constructor
        public Moto()
        {
            _maxFuel = 10;
            _factor = 0.05;
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
