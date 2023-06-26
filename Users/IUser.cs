using ApplicationNo1.Users.Vehicles;
using static ApplicationNo1.Users.Vehicles.VehicleBase;

namespace ApplicationNo1.Users
{
    public interface IUser
    {
       
        #region Properties
        string? Name { get; set; }
        int Age { get; set; }
        Wallet Wallet { get; set; }
        IVehicle Vehicle { get; set; }
        Country Country { get; set; }
        DateTime CreationTime { get; set; }
        #endregion

        #region Methods
        bool UserDrive(double distance);

        RefuelResults UserRefuel(double money);
        #endregion

    }
}
