using Judge.App.Models.Submissions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Judge.App.Services.Contracts
{
    public interface ISubmissionService
    {
        bool Create(string code, int contestId, int userId);
        List<SubmissionIsSuccess> All(int userId);
        //ContestEditModel Get(int id);
    }
}
