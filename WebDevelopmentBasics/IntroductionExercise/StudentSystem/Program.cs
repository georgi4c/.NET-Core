using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Extensions.Internal;
using StudentSystem.Data;
using StudentSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace StudentSystem
{
    class Program
    {
        private const int TotalStudents = 50;
        private const int TotalCourses = 15;
        private const int MinStudentsInCourse = 5;
        private const int MinResourcesInCourse = 30;
        private const int HomeworksToSeed = 500;

        private readonly Random random = new Random();

        static void Main(string[] args)
        {
            var context = new StudentSystemContext();
            context.Database.Migrate();
            
            SeedInitalData(context);

            //ListAllStudents(context);
            //ListAllCoursesWithResources(context);
            //ListAllCoursesWithMoreThan5Resourses(context);
            ListStudentCourses(context);

        }

        private static void ListStudentCourses(StudentSystemContext context)
        {
            var studentCourses = context.Students
                //.Where(s => s.Courses.Any())
                .Select(s => new
                {
                    s.Name,
                    CoursesCount = s.Courses.Count,
                    CoursesTotalPrice = s.Courses.DefaultIfEmpty().Sum(c => c.Course.Price),
                    CoursesAvgPrice = s.Courses.DefaultIfEmpty().Average(c => c.Course.Price)
                });
        }

        private static void ListAllCoursesWithMoreThan5Resourses(StudentSystemContext context)
        {
            var coursesWithMoreThan5Resourses = context.Courses
                .Where(c => c.Resources.Count > 5)
                .OrderByDescending(c => c.Resources.Count)
                .ThenByDescending(c => c.StartDate)
                .Select(c => new
                {
                    c.Name,
                    ResourseCount = c.Resources.Count
                });
        }

        private static void ListAllCoursesWithResources(StudentSystemContext context)
        {
            var coursesWithResources = context.Courses
                .OrderBy(c => c.StartDate)
                .ThenByDescending(c => c.EndDate)
                .Select(c => new
                {
                    c.Name,
                    c.Description,
                    c.Resources
                });

        }

        private static void ListAllStudents(StudentSystemContext context)
        {
            var studentsWithHomeworks = context.Students.Select(s => new
            {
                s.Name,
                Homeworks = s.Homeworks.Select(h => new
                {
                    h.Content,
                    h.ContentType
                })
            });
            foreach (var studentsWithHomework in studentsWithHomeworks)
            {
                Console.WriteLine($"{studentsWithHomework.Name} has {string.Join(", ", studentsWithHomework.Homeworks.Select(h => h.Content + "." + h.ContentType))}");
                foreach (var studentHomework in studentsWithHomework.Homeworks)
                {
                    Console.WriteLine($"-----{studentHomework.Content} - {studentHomework.ContentType}");
                }
            }
        }


        private static void SeedInitalData(StudentSystemContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            SeedStudents(context);

            SeedCourses(context);

            SeedResources(context);

            SeedHomeworks(context);

            SeedLicenses(context);
        }

        private static void SeedLicenses(StudentSystemContext context)
        {
            var resources = context.Resources.ToList();
            foreach (var resource in resources)
            {
                for (int i = 0; i < 5; i++)
                {
                    resource.Licenses.Add(new License { Name = $"License {resource.Id + i}" });
                }
            }

            context.SaveChanges();
        }

        private static void SeedHomeworks(StudentSystemContext context)
        {
            var students = context.Students.ToList();
            var courses = context.Courses.ToList();

            var homeworks = new List<Homework>();
            var random = new Random();
            for (int i = 0; i < HomeworksToSeed; i++)
            {
                var randomStudent = students[random.Next(0, students.Count)];
                var randomCourse = courses[random.Next(0, courses.Count)];

                if (!homeworks.Any(h => h.CourseId == randomCourse.Id && h.StudentId == randomStudent.Id))
                {
                    homeworks.Add(new Homework
                    {
                        StudentId = randomStudent.Id,
                        CourseId = randomCourse.Id,
                        Content = $"HC{randomStudent.Id + randomCourse.Id}",
                        ContentType = ContentType.Zip,
                        SubmissionDate = randomCourse.StartDate.AddDays(i)
                    });
                }
                else
                {
                    i--;
                }
            }

            context.Homeworks.AddRange(homeworks);
            context.SaveChanges();
        }

        private static void SeedResources(StudentSystemContext context)
        {
            var courses = context.Courses.ToList();

            foreach (var course in courses)
            {
                for (int i = 0; i < MinResourcesInCourse; i++)
                {
                    course.Resources.Add(new Resource
                    {
                        Name = $"Resource {i} {course.Id}",
                        Type = ResourceType.Document,
                        Url = $"ResUrl{i} {course.Id}"
                    });
                }
            }

            context.SaveChanges();
        }

        private static void SeedCourses(StudentSystemContext context)
        {
            var students = context.Students.ToList();
            var random = new Random();

            for (int i = 0; i < TotalCourses; i++)
            {
                var currentCourseStudents = new List<StudentCourse>();
                for (int j = 0; j < MinStudentsInCourse; j++)
                {
                    var randomStudentIndex = random.Next(0, students.Count);
                    var randomStudent = students[randomStudentIndex];

                    if (!currentCourseStudents.Any(s => s.StudentId == randomStudent.Id))
                    {
                        currentCourseStudents.Add(new StudentCourse { StudentId = randomStudent.Id });
                    }
                    else
                    {
                        j--;
                    }
                }

                var course = new Course
                {
                    Name = $"Course {i}",
                    Description = $"Course Description {i}",
                    Price = i * 10,
                    StartDate = DateTime.Now.AddDays(i),
                    EndDate = DateTime.Now.AddDays(40 + i),
                    Students = currentCourseStudents
                };
                context.Courses.Add(course);
            }

            context.SaveChanges();
        }

        private static void SeedStudents(StudentSystemContext context)
        {
            for (int i = 0; i < TotalStudents; i++)
            {
                context.Students.Add(new Student
                {
                    Name = $"Student Name {i}",
                    PhoneNumber = $"Student Phone Number {i}",
                    Birthday = DateTime.Now.AddYears(30 - i),
                    RegistrationDate = DateTime.Now.AddDays(-5 * i)
                });
            }

            context.SaveChanges();
        }
    }
}
