using ModPanel.App.Services.Contracts;
using SimpleMvc.Framework.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModPanel.App.Controllers
{
    public class AdminController : BaseController
    {
        private readonly IUserService users;

        public AdminController(IUserService users)
        {
            this.users = users;
        }
        public IActionResult Users()
        {
            if (!this.IsAdmin)
            {
                return this.RedirectToLogin();
            }

            var rows = this.users
                .All()
                .Select(u => $@"
                    <tr>
                        <td>{u.Id}</td>
                        <td>{u.Email}</td>
                        <td>{u.Position}</td>
                        <td>0</td>
                        <td>
                            {(u.IsApproved ? string.Empty : $@"<a class=""btn btn-primary btn-sm"" href=""/admin/approve?id={u.Id}"">Approve</a>")}
                        </td>
                    </tr>");

            this.ViewModel["users"] = string.Join(string.Empty, rows);

            return this.View();
        }
    }
}
