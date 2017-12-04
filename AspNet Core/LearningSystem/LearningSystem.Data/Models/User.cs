using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

using static LearningSystem.Data.DataConstants;

namespace LearningSystem.Data.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class User : IdentityUser
    {
        [Required]
        [MinLength(UserNameMinLength)]
        [MaxLength(UserNameMaxLength)]
        public string Name { get; set; }

        public DateTime Birthdate { get; set; }

        public List<Article> Articles { get; set; } = new List<Article>();

        public List<StudentCourse> Courses { get; set; } = new List<StudentCourse>();

        public List<Course> Trainings { get; set; } = new List<Course>();
    }
}
