using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FriendBookApi.Models
{
    public class FriendshipViewModel
    {
        
        public int UserID { get; set; }

        public int[] FriendIds { get; set; }
    }
}