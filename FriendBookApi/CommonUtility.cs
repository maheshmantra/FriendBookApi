using FriendBookApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace FriendBookApi
{
    public class CommonUtility : ApiController
    {

        public int CreateFriends(FriendshipViewModel friendViewModel) {

            UserFriend oUserFriend;
            List<UserFriend> userFriendList = new List<UserFriend>();
            try
            {
                using (FriendBookEntities fbEntities = new FriendBookEntities())
                {
                    foreach (int friendId in friendViewModel.FriendIds)
                    {
                        //to make a friend
                        oUserFriend = PrepareData(friendViewModel.UserID, friendId);
                        userFriendList.Add(oUserFriend);
                        //to become friend
                        oUserFriend = PrepareData(friendId, friendViewModel.UserID);
                        userFriendList.Add(oUserFriend);
                    }
                    fbEntities.UserFriends.AddRange(userFriendList);
                    int result = fbEntities.SaveChanges();
                    return result;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public  UserFriend PrepareData(int userId, int friendId)
        {
            UserFriend oUserFriend = new UserFriend();
            oUserFriend.UserId = userId;
            oUserFriend.FriendUserId = friendId;
            return oUserFriend;
        }

        public HttpResponseMessage ErrorHandler(string message) {
           // var message = string.Format("User with username = {0} not found", userName);
            HttpError err = new HttpError(message);
            return Request.CreateResponse(HttpStatusCode.NotFound, err);
        }
    }
}