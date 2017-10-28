namespace ModPanel.App.Data.Models
{
    using ModPanel.App.Data.Models.Enums;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(50)]
        public string Password { get; set; }

        [Required]
        public bool IsAdmin { get; set; }

        [Required]
        public bool IsApproved { get; set; }

        [Required]
        public Position Position { get; set; }
    }
}