using ApplicationNo1.Users.Vehicles;

namespace ApplicationNo1.Users
{
    public interface IUser
    {
        string? Name { get; set; }
        int Age { get; set; }
        Wallet Wallet { get; set; }
        IVehicle Vehicle { get; set; }
        Country Country { get; set; }
        DateTime CreationTime { get; set; }
    }
}
