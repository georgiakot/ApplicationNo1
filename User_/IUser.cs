using ApplicationNo1.Country_;
using ApplicationNo1.Trip_;
using ApplicationNo1.Vehicle_;
using ApplicationNo1.Wallet_;
using static ApplicationNo1.Vehicle_.VehicleBase;

namespace ApplicationNo1.User_
{
    public interface IUser
    {
       
        #region Properties
        string? Id { get; set; }
        string? Name { get; set; }
        int Age { get; set; }
        Wallet Wallet { get; set; }
        IVehicle IVehicle { get; set; }
        ITrip? Trip { get; set; }
        Country StartingCountry { get; }
        Country CurrentCountry { get; }
        DateTime CreationTime { get; set; }
        #endregion

        #region Methods
        bool Drive(double distance, Country countryDestination);
        RefuelResults Refuel(double money);
        #endregion

    }
}
