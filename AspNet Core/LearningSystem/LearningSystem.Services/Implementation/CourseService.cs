using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LearningSystem.Services.Models;
using LearningSystem.Data;
using System.Linq;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using LearningSystem.Data.Models;

namespace LearningSystem.Services.Implementation
{
    public class CourseService : ICourseService
    {
        private readonly LearningSystemDbContext db;

        public CourseService(LearningSystemDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<CourseListingServiceModel>> ActiveAsync()
            => await this.db
                .Courses
                .OrderByDescending(c => c.Id)
                .Where(c => c.StartDate >= DateTime.UtcNow)
                .ProjectTo<CourseListingServiceModel>()
                .ToListAsync();

        public async Task<TModel> ByIdAsync<TModel>(int id) where TModel : class
            => await this.db
                .Courses
                .Where(c => c.Id == id)
                .ProjectTo<TModel>()
                .FirstOrDefaultAsync();

        public async Task<bool> SignOutStudentAsync(int courseId, string studentId)
        {
            var courseInfo = await GetCourseInfoAsync(courseId, studentId);

            if (courseInfo == null
                || courseInfo.StartDate <= DateTime.UtcNow
                || !courseInfo.IsStudentEnrolled)
            {
                return false;
            }

            var studentCourse = await this.db.StudentCourses
                .Where(sc => 
                    sc.CourseId == courseId 
                    && sc.StudentId == studentId)
                .FirstOrDefaultAsync();

            if (studentCourse == null)
            {
                return false;
            }

            this.db.Remove(studentCourse);
            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> SignUpStudentAsync(int courseId, string studentId)
        {
            var courseInfo = await GetCourseInfoAsync(courseId, studentId);

            if (courseInfo == null
                ||courseInfo.StartDate < DateTime.UtcNow
                || courseInfo.IsStudentEnrolled)
            {
                return false;
            }

            var studentInCourse = new StudentCourse
            {
                CourseId = courseId,
                StudentId = studentId
            };

            this.db.Add(studentInCourse);

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> StudentIsEnrolledCourseAsync(int courseId, string studentId)
            => await this.db
                .Courses
                .AnyAsync(c => c.Id == courseId && c.Students.Any(s => s.StudentId == studentId));

        private async Task<CourseWithStudentInfo> GetCourseInfoAsync(int courseId, string studentId)
            => await this.db
                .Courses
                .Where(c => c.Id == courseId)
                .Select(c => new CourseWithStudentInfo
                {
                    StartDate = c.StartDate,
                    IsStudentEnrolled = c.Students.Any(s => s.StudentId == studentId)
                })
                .FirstOrDefaultAsync();
    }
}
