using System.Collections.Generic;
using CameraBazaar.Data.Models;
using CameraBazaar.Data;
using System.Linq;
using CameraBazaar.Services.Models;
using System;

namespace CameraBazaar.Services.Implementations
{
    public class CameraService : ICameraService
    {
        private CameraBazaarDbContext db;

        public CameraService(CameraBazaarDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<CameraMinifiedModel> All()
            => this.db.Cameras
                .Select(c => new CameraMinifiedModel()
                {
                    Id = c.Id,
                    Make = c.Make,
                    Model = c.Model,
                    Price = c.Price,
                    Quantity = c.Quantity,
                    ImageUrl = c.ImageUrl
                })
                .ToList();

        public IEnumerable<CameraMinifiedModel> AllBySeller(string id)
            => this.db.Cameras
                    .Where(c => c.UserId == id)
                    .Select(c => new CameraMinifiedModel()
                    {
                        Id = c.Id,
                        Make = c.Make,
                        Model = c.Model,
                        Price = c.Price,
                        Quantity = c.Quantity,
                        ImageUrl = c.ImageUrl
                    })
                    .ToList();

        public void Create(CameraMake make, string model, decimal price, int quantity, int minShutterSpeed, int maxShutterSpeed, MinISO minIso, int maxIso, bool isFullFrame, string videoResolution,IEnumerable<LightMetering> lightMeterings, string description, string imageUrl, string userId)
        {
            var camera = new Camera()
            {
                Make = make,
                Model = model,
                Price = price,
                Quantity = quantity,
                MinShutterSpeed = minShutterSpeed,
                MaxShutterSpeed = maxShutterSpeed,
                MinISO = minIso,
                MaxISO = maxIso,
                IsFullFrame = isFullFrame,
                VideoResolution = videoResolution,
                LightMetering = (LightMetering)lightMeterings.Cast<int>().DefaultIfEmpty().Sum(),
                Description = description,
                ImageUrl = imageUrl,
                UserId = userId
            };

            this.db.Add(camera);
            this.db.SaveChanges();
        }

        public bool Edit(int id, CameraMake make, string model, decimal price, int quantity, int minShutterSpeed, int maxShutterSpeed, MinISO minIso, int maxIso, bool isFullFrame, string videoResolution, IEnumerable<LightMetering> lightMeterings, string description, string imageUrl)
        {
            var camera = this.db
                .Cameras
                .FirstOrDefault(c => c.Id == id);

            if (camera == null)
            {
                return false;
            }

            camera.Make = make;
            camera.Model = model;
            camera.Price = price;
            camera.Quantity = quantity;
            camera.MinShutterSpeed = minShutterSpeed;
            camera.MaxShutterSpeed = maxShutterSpeed;
            camera.MinISO = minIso;
            camera.MaxISO = maxIso;
            camera.IsFullFrame = isFullFrame;
            camera.VideoResolution = videoResolution;
            camera.LightMetering = (LightMetering)lightMeterings.Cast<int>().DefaultIfEmpty().Sum();
            camera.ImageUrl = imageUrl;
            camera.Description = description;

            db.SaveChanges();

            return true;
        }

        public bool Exists(int id, string userId)
            => this.db.Cameras.Any(c => c.Id == id && c.UserId == userId);

        public CameraFullModel Get(int id)
            => db.Cameras
                .Where(c => c.Id == id)
                .Select(c => new CameraFullModel()
                {
                    Make = c.Make,
                    Model = c.Model,
                    Price = c.Price,
                    Quantity = c.Quantity,
                    MinShutterSpeed = c.MinShutterSpeed,
                    MaxShutterSpeed = c.MaxShutterSpeed,
                    MinISO = c.MinISO,
                    MaxISO = c.MaxISO,
                    IsFullFrame = c.IsFullFrame,
                    VideoResolution = c.VideoResolution,
                    LightMeterings = Enum.GetValues(typeof(LightMetering))
                                        .Cast<int>()
                                        .Where(lm => (lm & (int)c.LightMetering) == lm)
                                        .Cast<LightMetering>()
                                        .ToList(),
                    Description = c.Description,
                    ImageUrl = c.ImageUrl,
                    UserId = c.UserId,
                    UserName = c.User.UserName
                })
                .FirstOrDefault();
    }
}
