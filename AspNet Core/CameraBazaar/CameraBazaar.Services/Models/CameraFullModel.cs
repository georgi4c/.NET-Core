using CameraBazaar.Data.Models;
using System.Collections.Generic;

namespace CameraBazaar.Services.Models
{
    public class CameraFullModel
    {
        public CameraMake Make { get; set; }
        
        public string Model { get; set; }

        public decimal Price { get; set; }
        
        public int Quantity { get; set; }
        
        public int MinShutterSpeed { get; set; }
        
        public int MaxShutterSpeed { get; set; }
        
        public MinISO MinISO { get; set; }
        
        public int MaxISO { get; set; }

        public bool IsFullFrame { get; set; }
        
        public string VideoResolution { get; set; }

        public IEnumerable<LightMetering> LightMeterings { get; set; }
        
        public string Description { get; set; }
        
        public string ImageUrl { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }
    }
}
