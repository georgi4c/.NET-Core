using Judge.App.Services.Contracts;
using SimpleMvc.Framework.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SimpleMvc.Framework.Attributes.Methods;
using Judge.App.Models;

namespace Judge.App.Controllers
{
    public class SubmissionsController : BaseController
    {
        private IContestService contests;
        private ISubmissionService submissions;

        public SubmissionsController(ISubmissionService submissions, IContestService contests)
        {
            this.submissions = submissions;
            this.contests = contests;
        }
        public IActionResult Add()
        {
            if (!this.User.IsAuthenticated)
            {
                return this.RedirectToLogin();
            }

            var result = this.contests
                .All()
                .Select(c => $@"<option value=""{c.Id}"" selected>{c.Name}</option>")
                .ToList();
            this.ViewModel["contests"] = string.Join(string.Empty, result);

            return this.View();
        }


        [HttpPost]
        public IActionResult Add(SubmissionAddModel model)
        {
            if (!this.User.IsAuthenticated)
            {
                return this.RedirectToLogin();
            }

            this.submissions.Create(model.Code, model.Contest, this.Profile.Id);

            return this.Redirect("/submissions/all");
        }
        public IActionResult All()
        {
            if (!this.User.IsAuthenticated)
            {
                return this.RedirectToLogin();
            }
            var result = this.contests
                .All()
                .Select(c => $@"<a class=""list-group-item list-group-item-action list-group-item-dark active"" data-toggle=""list"" href=""#first-contest"" role=""tab"" >{c.Name}</a>")
                .ToList();
            this.ViewModel["contests"] = string.Join(string.Empty, result);

            return this.View();
        }
    }
}
