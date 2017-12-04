using CameraBazaar.Data.Models;
using CameraBazaar.Services;
using CameraBazaar.Services.Implementations;
using CameraBazaar.Web.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CameraBazaar.Web.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private UserManager<User> users;
        private ICameraService cameras;

        public UsersController(UserManager<User> users, ICameraService cameras)
        {
            this.users = users;
            this.cameras = cameras;
        }

        public IActionResult Edit(string id)
        {
            if (id != this.users.GetUserId(User))
            {
                return Unauthorized();
            }

            var user = Task.Run(() => this.users.FindByIdAsync(id))
                .GetAwaiter()
                .GetResult();


            if (user == null)
            {
                return this.NotFound();
            }

            var model = new UserEditModel()
            {
                Id = user.Id,
                Email = user.Email,
                Phone = user.PhoneNumber
            };

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, UserEditModel model)
        {
            if (id != this.users.GetUserId(User))
            {
                return Unauthorized();
            }

            var user = Task.Run(() => this.users.FindByIdAsync(id))
                .GetAwaiter()
                .GetResult();


            if (user == null)
            {
                return this.NotFound();
            }

            var isPasswordValid = Task.Run(() => this.users.CheckPasswordAsync(user, model.CurrentPassword))
                .GetAwaiter()
                .GetResult();
            if (!isPasswordValid)
            {
                return Unauthorized();
            }

            await this.users.ChangePasswordAsync(user, model.CurrentPassword, model.Password);

            var email = user.Email;
            if (model.Email != email)
            {
                var setEmailResult = await users.SetEmailAsync(user, model.Email);
                if (!setEmailResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting email for user with ID '{user.Id}'.");
                }
            }

            var phoneNumber = user.PhoneNumber;
            if (model.Phone != phoneNumber)
            {
                var setPhoneResult = await users.SetPhoneNumberAsync(user, model.Phone);
                if (!setPhoneResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting phone number for user with ID '{user.Id}'.");
                }
            }

            return RedirectToAction(nameof(WithCameras), new { id });
        }

        public IActionResult WithCameras(string id)
        {

            var user = Task.Run(() => this.users.FindByIdAsync(id))
                .GetAwaiter()
                .GetResult();

            if (user == null)
            {
                return this.NotFound();
            }

            if (id != users.GetUserId(User))
            {
                return this.Unauthorized();
            }

            var userModel = new UserWithCamerasModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Phone = user.PhoneNumber,
                Cameras = this.cameras.AllBySeller(id)
            };

            return View(userModel);
        }
    }
}
