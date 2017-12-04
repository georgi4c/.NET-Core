using CameraBazaar.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CameraBazaar.Web.Models.Users
{
    public class UserWithCamerasModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public IEnumerable<CameraMinifiedModel> Cameras { get; set; }
    }
}
