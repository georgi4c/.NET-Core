using CameraBazaar.Data.Models;
using CameraBazaar.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CameraBazaar.Services
{
    public interface ICameraService
    {
        IEnumerable<CameraMinifiedModel> All();

        IEnumerable<CameraMinifiedModel> AllBySeller(string id);

        void Create(
            CameraMake make,
            string model,
            decimal price,
            int quantity,
            int minShutterSpeed,
            int maxShutterSpeed,
            MinISO minIso,
            int maxIso,
            bool isFullFrame,
            string videoResolution,
            IEnumerable<LightMetering> lightMetering,
            string description,
            string imageUrl,
            string userId);

        CameraFullModel Get(int id);

        bool Edit(
            int id,
            CameraMake make,
            string model,
            decimal price,
            int quantity,
            int minShutterSpeed,
            int maxShutterSpeed,
            MinISO minIso,
            int maxIso,
            bool isFullFrame,
            string videoResolution,
            IEnumerable<LightMetering> lightMetering,
            string description,
            string imageUrl);

        bool Exists(int id, string userId);
    }
}
