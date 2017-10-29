using Judge.App.Models.Contests;
using Judge.App.Services.Contracts;
using SimpleMvc.Framework.Attributes.Methods;
using SimpleMvc.Framework.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Judge.App.Controllers
{
    public class ContestsController : BaseController
    {
        private IContestService contests;

        public ContestsController(IContestService users)
        {
            this.contests = users;
        }
        public IActionResult Add()
        {
            if (!this.User.IsAuthenticated)
            {
                return this.RedirectToLogin();
            }
            return this.View();
        }

        [HttpPost]
        public IActionResult Add(ContestModel model)
        {
            if (!this.User.IsAuthenticated)
            {
                return this.RedirectToLogin();
            }
            if (!this.IsValidModel(model))
            {
                this.ShowError("Name must begin with uppercase letter and has length between 3 and 100 symbols.");
                return this.View();
            }

            var isCreated = this.contests.Create(model.Name, this.Profile.Id);
            if (isCreated)
            {
                return this.Redirect("/contests/all");
            }
            else
            {
                this.ShowError("Name already exists.");
                return this.View();
            }
        }

        public IActionResult All()
        {
            if (!this.User.IsAuthenticated)
            {
                return this.RedirectToLogin();
            }
            var result = this.contests
                .All()
                .Select(c => $@"<tr><td scope=""row"">{c.Name}</td><td>{c.Id}</td><td><a href = ""/contests/edit?id={c.Id}"" class=""btn btn-sm btn-warning"" style=""display: {ShowOrHide(c.UserId)}""> Edit</a><a href = ""/contests/delete?id={c.Id}"" class=""btn btn-sm btn-danger"" style=""display: {ShowOrHide(c.UserId)}"">Delete</a></td></tr>")
                .ToList();
            this.ViewModel["contests"] = string.Join(string.Empty, result);

            return this.View();
        }

        private string ShowOrHide(int contestId)
        {
            if (contestId == this.Profile.Id || this.IsAdmin)
            {
                return "inline-block";
            }
            return "none";
        }

        public IActionResult Edit(int id)
        {
            if (!this.User.IsAuthenticated)
            {
                return this.RedirectToLogin();
            }
            if (!this.IsAdmin && !this.contests.IsAuthor(id, this.Profile.Id))
            {
                return this.Redirect("/contests/all");
            }

            var model = this.contests.Get(id);
            if (model == null)
            {
                this.ShowError("Invalid contest id.");
                return this.Redirect("/contests/all");
            }
            this.ViewModel["contestId"] = model.Id.ToString();
            this.ViewModel["contestName"] = model.Name;

            return this.View();
        }

        [HttpPost]
        public IActionResult Edit(ContestEditModel model)
        {
            if (!this.User.IsAuthenticated)
            {
                return this.RedirectToLogin();
            }
            if (!this.IsAdmin && !this.contests.IsAuthor(model.Id, this.Profile.Id))
            {
                return this.Redirect("/contests/all");
            }

            var result = this.contests.Edit(model.Id, model.Name);
            if (result)
            {
                return this.Redirect("/contests/all");
            }

            return this.Redirect($"/contests/edit?id={model.Id}");
        }

        public IActionResult Delete(int id)
        {
            if (!this.User.IsAuthenticated)
            {
                return this.RedirectToLogin();
            }
            if (!this.IsAdmin && !this.contests.IsAuthor(id, this.Profile.Id))
            {
                return this.Redirect("/contests/all");
            }
            var model = this.contests.Get(id);
            if (model == null)
            {
                this.ShowError("Invalid contest id.");
                return this.Redirect("/contests/all");
            }
            this.ViewModel["contestId"] = model.Id.ToString();
            this.ViewModel["contestName"] = model.Name;

            return View();
        }

        [HttpPost]
        public IActionResult Delete(ContestEditModel model)
        {
            if (!this.User.IsAuthenticated)
            {
                return this.RedirectToLogin();
            }
            if (!this.IsAdmin && !this.contests.IsAuthor(model.Id, this.Profile.Id))
            {
                return this.Redirect("/contests/all");
            }

            this.contests.Delete(model.Id);

            return this.Redirect("/contests/all");
        }

    }
}
