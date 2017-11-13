using Judge.App.Data;
using Judge.App.Data.Models;
using Judge.App.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Judge.App.Models.Submissions;

namespace Judge.App.Services
{
    public class SubmissionService : ISubmissionService
    {
        private readonly JudgeDbContext db;
        private Random random;

        public SubmissionService(JudgeDbContext db)
        {
            this.random = new Random();
            this.db = db;
        }

        public List<SubmissionIsSuccess> All(int userId)
        {
            return this.db.Submissions.Where(s => s.UserId == userId).Select(s => new SubmissionIsSuccess() { IsSuccess = s.IsSuccess }).ToList();
        }

        public bool Create(string code, int contestId, int userId)
        {
            if (db.Submissions.Any(c => c.Code == code))
            {
                return false;
            }
            var n = random.Next(0,100);
            var isSuccess = n > 70;
            db.Submissions.Add(new Submission { Code = code, ContestId = contestId, IsSuccess = isSuccess, UserId = userId });
            db.SaveChanges();

            return true;
        }
    }
}
