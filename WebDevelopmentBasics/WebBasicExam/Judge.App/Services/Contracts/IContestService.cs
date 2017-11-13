using Judge.App.Data.Models;
using Judge.App.Models.Contests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Judge.App.Services.Contracts
{
    public interface IContestService
    {
        bool Create(string name, int userId);
        List<ContestListingModel> All();
        List<ContestListingModel> All(int userId);
        ContestEditModel Get(int id);
        bool Edit(int id, string name);
        bool IsAuthor(int id, int userId);
        bool Delete(int id);
    }
}
