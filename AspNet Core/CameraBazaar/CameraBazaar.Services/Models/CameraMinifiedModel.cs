using CameraBazaar.Data.Models;

namespace CameraBazaar.Services.Models
{
    public class CameraMinifiedModel
    {
        public int Id { get; set; }

        public CameraMake Make { get; set; }
        
        public string Model { get; set; }

        public decimal Price { get; set; }
        
        public int Quantity { get; set; }

        public string ImageUrl { get; set; }
    }
}
