namespace ApplicationNo1.User_
{
    public class UserService
    {

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

    }
}
