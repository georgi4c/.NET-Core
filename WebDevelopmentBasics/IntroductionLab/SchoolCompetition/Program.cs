using System;
using System.Collections.Generic;
using System.Linq;

namespace SchoolCompetition
{
    class Program
    {
        static void Main()
        {
            var studentScoresByStudentName = new Dictionary<string, int>();
            var studentCategoriesByStudentName = new Dictionary<string, SortedSet<string>>();

            while (true)
            {
                var line = Console.ReadLine();
                if (line == "END")
                {
                    break;
                }

                var studentData = line.Split(' ');
                var studentName = studentData[0];
                var studentCategory = studentData[1];
                var studentScore = int.Parse(studentData[2]);

                if (!studentScoresByStudentName.ContainsKey(studentName))
                {
                    studentScoresByStudentName.Add(studentName, 0);
                }

                if (!studentCategoriesByStudentName.ContainsKey(studentName))
                {
                    studentCategoriesByStudentName.Add(studentName, new SortedSet<string>());
                }

                studentScoresByStudentName[studentName] += studentScore;
                studentCategoriesByStudentName[studentName].Add(studentCategory);
            }

            var orderedStudents = studentScoresByStudentName
                .OrderByDescending(kvp => kvp.Value)
                .ThenBy(s => s.Key);

            foreach (var kvp in orderedStudents)
            {
                string studentCategoriesAsText = string.Join(", ", studentCategoriesByStudentName[kvp.Key]);
                string studentInfoLine = $"{kvp.Key}: {kvp.Value} [{studentCategoriesAsText}]";
                Console.WriteLine(studentInfoLine);
            }

        }
    }
}
