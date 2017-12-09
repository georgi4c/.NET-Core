using BookShop.Api.Infrastructures.Extensions;
using BookShop.Api.Infrastructures.Filters;
using BookShop.Api.Models.Books;
using BookShop.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

using static BookShop.Api.WebConstants;

namespace BookShop.Api.Controllers
{
    public class BooksController : BaseController
    {
        public readonly IBookService books;
        public readonly IAuthorService authors;

        public BooksController(IBookService books, IAuthorService authors)
        {
            this.books = books;
            this.authors = authors;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]string search = "")
           => this.Ok(await this.books.All(search));

        [HttpGet(WithId)]
        public async Task<IActionResult> Get(int id)
            => this.OkOrNotFound(await this.books.Details(id));

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Post([FromBody]BookRequestModel model)
        {
            if (!await this.authors.Exists(model.AuthorId))
            {
                return BadRequest("Author does not exist.");
            }

            var id = await this.books.Create(
                model.Title,
                model.Description,
                model.Price,
                model.Copies,
                model.Edition,
                model.AgeRestriction,
                model.ReleaseDate,
                model.AuthorId,
                model.Categories);

            return Ok(id);
        }

        [HttpPut(WithId)]
        [ValidateModelState]
        public async Task<IActionResult> Put(int id, [FromBody]BookRequestModel model)
        {
            if (!await this.authors.Exists(model.AuthorId))
            {
                return BadRequest("Author does not exist.");
            }
            if (!await this.books.Exists(model.AuthorId))
            {
                return BadRequest("Book does not exist.");
            }

            await this.books.Edit(
                id,
                model.Title,
                model.Description,
                model.Price,
                model.Copies,
                model.Edition,
                model.AgeRestriction,
                model.ReleaseDate,
                model.AuthorId,
                model.Categories);

            return Ok();
        }

        [HttpDelete(WithId)]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await this.books.Delete(id);
            if (!success)
            {
                return BadRequest("Book does not exist");
            }

            return Ok();
        }
    }
}
