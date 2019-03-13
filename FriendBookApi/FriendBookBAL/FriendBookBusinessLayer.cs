using FriendBookApi.FriendBookDAL;
using FriendBookApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace FriendBookApi.FriendBookBAL
{
    public class FriendBookBusinessLayer :IFriendBookBAL 
    {
        private IFriendBookDAL _objectDAL;
        public FriendBookBusinessLayer(IFriendBookDAL objectDAL)
        {
            _objectDAL = objectDAL;
        }
        public UserProfileViewModel GetUserProfileByUserName(string userName)
        {
            return _objectDAL.GetUserProfileByUserName(userName);
        }
        public IEnumerable<User> FindFriends(string searchvalue)
        {
            return _objectDAL.FindFriends(searchvalue);
        }
        public int MakeFriends(FriendshipViewModel friendViewModel)
        {
            return _objectDAL.MakeFriends(friendViewModel);
        }
        public int UnFriendInBulk(FriendshipViewModel friendViewModel)
        {
            return _objectDAL.UnFriendInBulk(friendViewModel);
        }
    }
}