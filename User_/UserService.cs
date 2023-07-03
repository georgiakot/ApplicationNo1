using ApplicationNo1.Country_;

namespace ApplicationNo1.User_
{
    public class UserService : IUserService
    {
        private List<IUser> _iusersList;

        public UserService()
        {
            _iusersList = new List<IUser>();
        }

        public List<IUser> Users { get { return _iusersList; } }

        public void AddNewUser(IUser user)
        {
            _iusersList.Add(user);
        }

        public IUser GetUserById(string userId)
        {
            return _iusersList.FirstOrDefault(x => x.Id == userId);
        }

        public Country GetUserCurrentCountry(string userId)
        { 
            
            //Call Trip Service For User Id

            //Get Last Trip Step, and the Country landed

            //Return That country
        }

    }
}

//SINGLETON PATTERN SOLUTION

/*
 
        private static UserService? _instance;
        private List<IUser> _iusersList;

        private UserService()
        {
            _iusersList = new List<IUser>();
        }

        public static UserService Instance 
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UserService();
                }
                return _instance;
            }
        }

        public List<IUser> IUsersList { get { return _iusersList; } }

        public void AddNewUser(IUser User)
        {
            _iusersList.Add(User);
        }
*/
