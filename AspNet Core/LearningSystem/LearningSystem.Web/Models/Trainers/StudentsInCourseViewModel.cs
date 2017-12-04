﻿using LearningSystem.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningSystem.Web.Models.Trainers
{
    public class StudentsInCourseViewModel
    {
        public IEnumerable<StudentInCourseServiceModel> Studetns { get; set; }

        public CourseListingServiceModel Course { get; set; }
    }
}
