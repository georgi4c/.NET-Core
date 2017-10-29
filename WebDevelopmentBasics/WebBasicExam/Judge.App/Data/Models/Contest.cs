using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Judge.App.Data.Models
{
    public class Contest
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public List<Submission> Submissions { get; set; }
    }
}
