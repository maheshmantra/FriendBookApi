using FriendBookApi.FriendBookBAL;
using FriendBookApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace FriendBookApi.Controllers
{
    [Authorize]
    public class UserController : ApiController
    {
        private readonly IFriendBookBAL _objectFriendBookBAL;
        

        public UserController(IFriendBookBAL objectFriendBookBAL)
        {
            _objectFriendBookBAL = objectFriendBookBAL;
        } 
        CommonUtility oCommon = new FriendBookApi.CommonUtility();
        [Route("User/{userName}")]
        public HttpResponseMessage GetUserProfileByUserName(string userName)
        {
            UserProfileViewModel userProfile = _objectFriendBookBAL.GetUserProfileByUserName(userName);
            if (userProfile == null)
            {
                var message = string.Format("User with username = {0} not found", userName);
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, userProfile);
            }
        }

        [HttpGet]
        [Route("User/FindFriends/{searchvalue}")]
        public HttpResponseMessage FindFriends(string searchvalue)
        {
            IEnumerable<User> userList = _objectFriendBookBAL.FindFriends(searchvalue);
            if (userList == null || userList.Count() == 0)
            {
                var message = string.Format("No Record found", searchvalue);
                HttpError err = new HttpError(message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, userList);
            }
        }

        [HttpPost]
        [Route("User/MakeFriends")]
        public HttpResponseMessage MakeFriends(FriendshipViewModel friendViewModel)
        {
            int result = _objectFriendBookBAL.MakeFriends(friendViewModel);

            if (result > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Friends relationship created");
            }
            else
            {
                HttpError err = new HttpError("There was issue creating friend relationship");
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }


        }

        [HttpPost]
        [Route("User/unfriend")]
        public HttpResponseMessage UnFriendInBulk(FriendshipViewModel friendViewModel)
        {
            int result = _objectFriendBookBAL.UnFriendInBulk(friendViewModel);
            if (result > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "User's friendship relation removed");
            }
            else
            {
                HttpError err = new HttpError("There was issue removing a user as a friend");
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
        }
    }
}
