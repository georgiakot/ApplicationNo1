using ApplicationNo1.Country_;
using ApplicationNo1.Trip_;
using ApplicationNo1.Vehicle_;
using ApplicationNo1.Wallet_;

namespace ApplicationNo1.User_
{
    public interface IUser
    {
        #region Properties
        string? Id { get; set; }
        string? Name { get; set; }
        int Age { get; set; }
        IWallet Wallet { get; set; }
        IVehicle Vehicle { get; set; }
        Country StartingCountry { get; }
        DateTime CreationTime { get; set; }
        #endregion
    }
}
