namespace SocialNetwork
{
    using SocialNetwork.Data;
    using SocialNetwork.Models;
    using System;
    using System.Linq;

    public class Program
    {
        private const int TotalUsers = 50;
        private const int MinFriendsPerUser = 2;
        private const int MaxFriendsPerUser = 10;

        public static void Main()
        {
            var context = new SocialNetworkDbContext();

            // SeedData(context);

            // 02.01 List all users with the count of their friends
            // PrintAllUsersWithFriendsCount(context);

            // 02.02 List all active usres with more than 5 friends
            // PrintAllActiveUsersWithMoreThan5Friends(context);
        }

        private static void PrintAllActiveUsersWithMoreThan5Friends(SocialNetworkDbContext context)
        {
            var users = context.Users
                .Where(u => !u.IsDeleted)
                .Where(u => u.Friends.Count > 5)
                .OrderBy(u => u.RegisteredOn)
                .ThenByDescending(u => u.Friends.Count)
                .Select(u => new
                {
                    Name = u.Username,
                    NumberOfFriends = u.Friends.Count,
                    Period = DateTime.Now.Subtract(u.RegisteredOn)
                })
                .ToList();

            foreach (var user in users)
            {
                Console.WriteLine($"{user.Name} - {user.NumberOfFriends} - {user.Period.TotalDays}");
            }
        }

        private static void PrintAllUsersWithFriendsCount(SocialNetworkDbContext context)
        {
            var users = context.Users
                .Select(u => new
                {
                    Name = u.Username,
                    NumberOfFriends = u.Friends.Count,
                    Status = u.IsDeleted ? "Inactive" : "Active"
                })
                .OrderByDescending(u => u.NumberOfFriends)
                .ThenBy(u => u.Name)
                .ToList();

            foreach (var user in users)
            {
                Console.WriteLine($"{user.Name} - Friends: {user.NumberOfFriends} - Status {user.Status}");
            }
        }

        private static void SeedData(SocialNetworkDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            SeedUsers(context);
            SeedFriends(context);
        }

        private static void SeedFriends(SocialNetworkDbContext context)
        {
            var users = context.Users.ToList();
            var random = new Random();

            foreach (var user in users)
            {
                for (int i = 0; i < random.Next(MinFriendsPerUser, MaxFriendsPerUser); i++)
                {
                    var randomFriend = users[random.Next(0, users.Count)];

                    if (!user.Friends.Any(f => f.FriendId == randomFriend.Id))
                    {
                        user.Friends.Add(new UserFriend { FriendId = randomFriend.Id });
                    }
                    else
                    {
                        i--;
                    }
                }
            }

            context.SaveChanges();
        }

        private static void SeedUsers(SocialNetworkDbContext context)
        {
            for (int i = 0; i < TotalUsers; i++)
            {
                context.Users.Add(new User
                {
                    Username = $"User {i}",
                    Age = 5 + i,
                    Email = $"user.{i}@gmail.com",
                    IsDeleted = false,
                    RegisteredOn = DateTime.Now.AddDays(-i),
                    LastTimeLoggedIn = DateTime.Now,
                    Password = "123"
                });
            }

            context.SaveChanges();
        }
    }
}
