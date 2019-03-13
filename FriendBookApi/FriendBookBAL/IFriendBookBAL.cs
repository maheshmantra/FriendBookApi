using FriendBookApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FriendBookApi.FriendBookBAL
{
    public interface IFriendBookBAL
    {
        UserProfileViewModel GetUserProfileByUserName(string userName);
        IEnumerable<User> FindFriends(string searchvalue);
        int MakeFriends(FriendshipViewModel friendViewModel);
        int UnFriendInBulk(FriendshipViewModel friendViewModel);

    }
}
