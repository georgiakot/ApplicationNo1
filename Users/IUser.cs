using ApplicationNo1.Trip;
using ApplicationNo1.Users.Vehicles;
using static ApplicationNo1.Users.Vehicles.VehicleBase;

namespace ApplicationNo1.Users
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
        Country CurrentCountry { get; set; }
        DateTime CreationTime { get; set; }
        #endregion

        #region Methods
        bool Drive(double distance, Country countryDestination);
        RefuelResults Refuel(double money);
     //   void NewTrip(Country country, double distance);
        #endregion

    }
}
