﻿using ApplicationNo1.Country_;
using ApplicationNo1.Trip_;

namespace ApplicationNo1.User_
{
    public class UserService : IUserService
    {
        #region Fields
        private List<IUser> _iusersList;
        private ITripService _tripService;
        #endregion

        #region Constructor
        public UserService(ITripService tripService)
        {
            _iusersList = new List<IUser>();
            _tripService = tripService;
        }
        #endregion

        #region Properties
        public List<IUser> Users { get { return _iusersList; } }
        #endregion

        #region Methods
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
            if (_tripService.Trips.Count == 0)
            {
                return GetUserById(userId).StartingCountry;
            }
            else
            {
                return _tripService.Trips.FirstOrDefault(x => x.UserID == userId).Steps.Last().CountryLanded;
            }
        }
        #endregion
    }
}


#region Singleton Pattern Solution

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
#endregion


