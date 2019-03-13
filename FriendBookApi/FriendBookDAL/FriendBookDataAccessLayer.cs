using FriendBookApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendBookApi.FriendBookDAL
{
    public class FriendBookDataAccessLayer : IFriendBookDAL
    {
        public UserProfileViewModel GetUserProfileByUserName(string userName)
        {
            using (FriendBookEntities friendBook = new FriendBookApi.FriendBookEntities())
            {
                User user = friendBook.Users.FirstOrDefault(u => u.UserName == userName);
                if (user != null)
                {
                    var oUserFriends = friendBook.UserFriends.Where(c => c.UserId == user.UserId);
                    UserProfileViewModel userProfile = new UserProfileViewModel();
                    userProfile.user = user;
                    List<User> userFriendList = new List<User>();
                    foreach (UserFriend userFriend in oUserFriends)
                    {
                        userFriendList.Add(friendBook.Users.FirstOrDefault(u => u.UserId == userFriend.FriendUserId));
                    }

                    userProfile.friends = userFriendList;

                    return userProfile;
                }

            }
            return null;
        }
        public IEnumerable<User> FindFriends(string searchvalue)
        {
            using (FriendBookEntities friendBook = new FriendBookApi.FriendBookEntities())
            {

                return friendBook.Users.Where(c => c.UserName.Contains(searchvalue) || c.FirstName.Contains(searchvalue)
                                        || c.LastName.Contains(searchvalue)).ToList();

            }
        }
        public int MakeFriends(FriendshipViewModel friendViewModel)
        {
            return CreateFriends(friendViewModel);
        }
        public int UnFriendInBulk(FriendshipViewModel friendViewModel) {
            
            using (var ctx = new FriendBookEntities())
            {
                var friends = ctx.UserFriends.Where(u => u.UserId == friendViewModel.UserID &&
                  friendViewModel.FriendIds.Contains(u.FriendUserId) || 
                  friendViewModel.FriendIds.Contains(u.UserId) && u.FriendUserId == friendViewModel.UserID
                  ).ToList();
                ctx.UserFriends.RemoveRange(friends);
                return ctx.SaveChanges();
                 
            }
        }

        private int CreateFriends(FriendshipViewModel friendViewModel)
        {

            UserFriend oUserFriend;
            List<UserFriend> userFriendList = new List<UserFriend>();
            CommonUtility oCommon = new FriendBookApi.CommonUtility();
            int result = 0;
            try
            {
                using (FriendBookEntities fbEntities = new FriendBookEntities())
                {
                    foreach (int friendId in friendViewModel.FriendIds)
                    {
                        //to make a friend
                        var friend = fbEntities.UserFriends.Where(u => u.UserId == friendViewModel.UserID && u.FriendUserId == friendId);
                        if (friend.Count() <= 0)
                        {
                            oUserFriend = oCommon.PrepareData(friendViewModel.UserID, friendId);
                            userFriendList.Add(oUserFriend);
                        }

                        friend = fbEntities.UserFriends.Where(u => u.UserId == friendId && u.FriendUserId == friendViewModel.UserID);
                        if (friend.Count() <= 0)
                        {
                            //to become friend
                            oUserFriend = oCommon.PrepareData(friendId, friendViewModel.UserID);
                            userFriendList.Add(oUserFriend);
                        }
                    }
                    if (userFriendList.Count > 0)
                    {
                        fbEntities.UserFriends.AddRange(userFriendList);
                        result = fbEntities.SaveChanges();
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
