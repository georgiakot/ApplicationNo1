using ApplicationNo1.Users.Vehicles;

namespace ApplicationNo1.Users
{
    public interface IUser
    {
        /*
        double Balance { get; set; }
        
        string? Currency { get; set; }

        double KmCounter { get; set; }

        string? CountryName { get; set; }

       // string? VehicleName { get; set; }

        */

        string? Name { get; set; }
        int Age { get; set; }
        Wallet Wallet { get; set; }
        IVehicle Vehicle { get; set; }
        Country Country { get; set; }
        DateTime CreationTime { get; set; }
    }
}
