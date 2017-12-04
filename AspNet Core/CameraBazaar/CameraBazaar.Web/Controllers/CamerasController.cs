using CameraBazaar.Data.Models;
using CameraBazaar.Services;
using CameraBazaar.Services.Models;
using CameraBazaar.Web.Models.Cameras;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CameraBazaar.Web.Controllers
{
    public class CamerasController : Controller
    {
        private readonly ICameraService cameras;
        private readonly UserManager<User> userManager;

        public CamerasController(ICameraService cameras, UserManager<User> userManager)
        {
            this.cameras = cameras;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Add() => this.View();

        [Authorize]
        [HttpPost]
        public IActionResult Add(AddCameraViewModel cameraModel)
        {
            if (!ModelState.IsValid)
            {
                return View(cameraModel);
            }

            this.cameras.Create(cameraModel.Make,
                cameraModel.Model,
                cameraModel.Price,
                cameraModel.Quantity,
                cameraModel.MinShutterSpeed,
                cameraModel.MaxShutterSpeed,
                cameraModel.MinISO,
                cameraModel.MaxISO,
                cameraModel.IsFullFrame,
                cameraModel.VideoResolution,
                cameraModel.LightMeterings,
                cameraModel.Description,
                cameraModel.ImageUrl,
                this.userManager.GetUserId(User));

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public IActionResult All()
        {
            var allCameras = this.cameras.All();

            return View(allCameras);
        }

        public  IActionResult Details(int id)
        {
            var camera = this.cameras.Get(id);

            if (camera == null)
            {
                return NotFound();
            }

            return View(camera);
        }
              
        [Authorize]
        public IActionResult Edit(int id)
        {
            var cameraExists = this.cameras.Exists(id, this.userManager.GetUserId(User));

            if (!cameraExists)
            {
                return NotFound();
            }
            var model = this.cameras.Get(id);
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(int id, CameraFullModel cameraModel)
        {
            var camera = this.cameras.Get(id);

            if (camera.UserId != this.userManager.GetUserId(User))
            {
                return NotFound();
            }

            var success = this.cameras.Edit(
                id,
                cameraModel.Make,
                cameraModel.Model,
                cameraModel.Price,
                cameraModel.Quantity,
                cameraModel.MinShutterSpeed,
                cameraModel.MaxShutterSpeed,
                cameraModel.MinISO,
                cameraModel.MaxISO,
                cameraModel.IsFullFrame,
                cameraModel.VideoResolution,
                cameraModel.LightMeterings,
                cameraModel.Description,
                cameraModel.ImageUrl);

            if (!success)
            {
                return View(cameraModel);
            }

            return RedirectToAction(nameof(All));            
        }
    }
}
