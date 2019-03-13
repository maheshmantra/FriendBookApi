using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FriendBookApi.Models
{
    public class UserProfileViewModel
    {
        public User user { get; set; }
        public IEnumerable<User> friends { get; set; }
    }
}