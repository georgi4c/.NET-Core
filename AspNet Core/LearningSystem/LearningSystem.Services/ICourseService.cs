using LearningSystem.Services.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearningSystem.Services
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseListingServiceModel>> ActiveAsync();

        Task<TModel> ByIdAsync<TModel>(int id) where TModel : class;

        Task<bool> SignUpStudentAsync(int courseId, string studentId);

        Task<bool> SignOutStudentAsync(int courseId, string studentId);

        Task<bool> StudentIsEnrolledCourseAsync(int courseId, string studentId);
    }
}
