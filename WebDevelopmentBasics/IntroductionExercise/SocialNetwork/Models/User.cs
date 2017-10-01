namespace SocialNetwork.Models
{
    using System;
    using System.Collections.Generic;

    public class User
    {
        public User()
        {
            this.Friends = new List<UserFriend>();
            this.FriendTo = new List<UserFriend>();
        }

        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public byte[] ProfilePicture { get; set; }

        public DateTime RegisteredOn { get; set; }

        public DateTime LastTimeLoggedIn { get; set; }

        public int Age { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<UserFriend> FriendTo { get; set; }

        public ICollection<UserFriend> Friends { get; set; }
    }
}
