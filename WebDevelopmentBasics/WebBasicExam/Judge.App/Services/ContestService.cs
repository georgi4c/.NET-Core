using Judge.App.Data;
using Judge.App.Data.Models;
using Judge.App.Models.Contests;
using Judge.App.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Judge.App.Services
{
    public class ContestService : IContestService
    {
        private readonly JudgeDbContext db;

        public ContestService(JudgeDbContext db)
        {
            this.db = db;
        }
        public bool Create(string name, int userId)
        {
            if (db.Contests.Any(c => c.Name == name))
            {
                return false;
            }

            db.Contests.Add(new Contest { Name = name, UserId = userId });
            db.SaveChanges();

            return true;
        }

        public List<ContestListingModel> All()
        {
            return db.Contests
                .Select(c => new ContestListingModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    UserId = c.UserId,
                    Submissions = c.Submissions.DefaultIfEmpty().Count()
                })
                .ToList();
        }

        public ContestEditModel Get(int id)
        {
            var contest = db.Contests.Find(id);
            if (contest == null)
            {
                return null;
            }
            var result = new ContestEditModel()
            {
                Id = contest.Id,
                Name = contest.Name
            };
            return result;
        }

        public bool Edit(int id, string name)
        {
            if (db.Contests.Any(c => c.Name == name) ||
                !db.Contests.Any(c => c.Id == id))
            {
                return false;
            }

            var content = db.Contests.Find(id);
            content.Name = name;
            db.SaveChanges();
            return true;
        }

        public bool IsAuthor(int id, int userId)
        {
            var result = this.db.Contests.Find(id);
            if (result == null)
            {
                return false;
            }
            return result.UserId == userId;
        }
        public bool Delete(int id)
        {
            var result = this.db.Contests.Find(id);
            if (result == null)
            {
                return false;
            }
            this.db.Contests.Remove(result);
            this.db.SaveChanges();
            return true;
        }

        public List<ContestListingModel> All(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
