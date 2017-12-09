using BookShop.Api.Infrastructures.Extensions;
using BookShop.Api.Infrastructures.Filters;
using BookShop.Services;
using BookShop.Services.Models.Categories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using static BookShop.Api.WebConstants;

namespace BookShop.Api.Controllers
{
    public class CategoriesController : BaseController
    {
        private readonly ICategoryService categories;

        public CategoriesController(ICategoryService categories)
        {
            this.categories = categories;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
            => this.Ok(await this.categories.All());

        [HttpGet(WithId)]
        public async Task<IActionResult> Get(int id)
            => this.OkOrNotFound(await this.categories.Get(id));

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Post([FromBody]CategoryDetailsServiceModel model)
        {
            var success = await this.categories.Create(model.Name);
            if (!success)
            {
                return BadRequest("Name already exist");
            }

            return Ok();
        }

        [HttpPut(WithId)]
        [ValidateModelState]
        public async Task<IActionResult> Put(int id, [FromBody]CategoryDetailsServiceModel model)
        {
            var success = await this.categories.Edit(id, model.Name);
            if (!success)
            {
                return BadRequest("Name already exist or category's id is invalid!");
            }

            return Ok();
        }

        [HttpDelete(WithId)]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await this.categories.Delete(id);
            if (!success)
            {
                return BadRequest("Id does not exist");
            }

            return Ok();
        }
    }
}
