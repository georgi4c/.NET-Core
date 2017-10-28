namespace ModPanel.App.Services
{
    using AutoMapper.QueryableExtensions;
    using Contracts;
    using Data;
    using Data.Models;
    using ModPanel.App.Data.Models.Enums;
    using ModPanel.App.Models.Admin;
    using System.Collections.Generic;
    using System.Linq;

    public class UserService : IUserService
    {
        private readonly ModPanelDbContext db;

        public UserService(ModPanelDbContext db)
        {
            this.db = db;
        }

        public bool Create(string email, string password, Position position)
        {
            if (this.db.Users.Any(u => u.Email == email))
            {
                return false;
            }

            var isAdmin = !db.Users.Any();

            var user = new User
            {
                Email = email,
                Password = password,
                IsAdmin = isAdmin,
                Position = position
            };

            db.Add(user);
            db.SaveChanges();

            return true;
        }

        public bool UserExists(string email, string password)
            => this.db
                .Users
                .Any(u => u.Email == email && u.Password == password);


        public IEnumerable<AdminUserModel> All()
            => this.db
                .Users
                .ProjectTo<AdminUserModel>()
                .ToList();
    }
}
