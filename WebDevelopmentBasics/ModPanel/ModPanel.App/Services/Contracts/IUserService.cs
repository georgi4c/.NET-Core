using ModPanel.App.Data.Models.Enums;
using ModPanel.App.Models.Admin;
using System.Collections.Generic;

namespace ModPanel.App.Services.Contracts
{
    public interface IUserService
    {
        bool Create(string email, string password, Position position);

        bool UserExists(string email, string password);

        IEnumerable<AdminUserModel> All();
    }
}
