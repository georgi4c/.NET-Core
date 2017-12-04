using CameraBazaar.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CameraBazaar.Web.Models.Cameras
{
    public class AddCameraViewModel
    {
        public CameraMake Make { get; set; }

        [Required]
        [MaxLength(100)]
        public string Model { get; set; }

        public decimal Price { get; set; }

        [Range(0, 100)]
        public int Quantity { get; set; }

        [Display(Name = "Min Shutter Speed")]
        [Range(1, 30)]
        public int MinShutterSpeed { get; set; }

        [Display(Name = "Max Shutter Speed")]
        [Range(2000, 8000)]
        public int MaxShutterSpeed { get; set; }

        [Display(Name = "Min ISO")]
        [Range(50, 100)]
        public MinISO MinISO { get; set; }

        [Display(Name = "Max ISO")]
        [Range(200, 409600)]
        public int MaxISO { get; set; }

        [Display(Name = "Full Frame")]
        public bool IsFullFrame { get; set; }

        [Display(Name = "Video Resolution")]
        [Required]
        [MaxLength(15)]
        public string VideoResolution { get; set; }

        [Display(Name = "Light Metering")]
        public IEnumerable<LightMetering> LightMeterings { get; set; }

        [Required]
        [MaxLength(6000)]
        public string Description { get; set; }

        [Display(Name = "Image URL")]
        [Required]
        [StringLength(2000, MinimumLength = 10)]
        public string ImageUrl { get; set; }
    }
}
