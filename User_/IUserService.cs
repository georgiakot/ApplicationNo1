using ApplicationNo1.Country_;
using Microsoft.Extensions.Hosting;

namespace ApplicationNo1.User_
{ 
    public interface IUserService
    {
        List<IUser> Users {  get; }
        void AddNewUser(IUser User);
        IUser GetUserById(string userId);
        Country GetUserCurrentCountry(string userId);
    }
}
