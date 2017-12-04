using Ganss.XSS;
using LearningSystem.Data.Models;
using LearningSystem.Services.Blog;
using LearningSystem.Services.Html;
using LearningSystem.Web.Areas.Blog.Models.Articles;
using LearningSystem.Web.Infrastructure.Extesions;
using LearningSystem.Web.Infrastructure.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static LearningSystem.Web.WebConstants;

namespace LearningSystem.Web.Areas.Blog.Controllers
{
    [Area(BlogArea)]
    [Authorize(Roles = BlogAuthorRole)]
    public class ArticlesController : Controller
    {
        private readonly IHtmlService html;
        private readonly IBlogArticleService articles;
        private readonly UserManager<User> userManager;

        public ArticlesController(IHtmlService html, IBlogArticleService articles, UserManager<User> userManager)
        {
            this.html = html;
            this.articles = articles;
            this.userManager = userManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(int page = 1)
            => View(new ArticleListingViewModel
            {
                Articles = await this.articles.AllAsync(page),
                TotalArticles = await this.articles.TotalAsync(),
                CurrentPage = page
            });

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Create(PublishArticleFormModel model)
        {
            model.Content = this.html.Sanitize(model.Content);

            var userId = this.userManager.GetUserId(User);

            await this.articles.CreteAsync(model.Title, model.Content, userId);

            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
            => this.ViewOrNotFound(await this.articles.ById(id));
    }    
}
