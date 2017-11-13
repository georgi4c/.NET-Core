namespace Judge.App.Data.Models
{
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

        [MaxLength(100)]
        public string FullName { get; set; }

        public bool IsAdmin { get; set; }

        public List<Contest> Contests { get; set; }

        public List<Submission> Submissions { get; set; }
    }
}