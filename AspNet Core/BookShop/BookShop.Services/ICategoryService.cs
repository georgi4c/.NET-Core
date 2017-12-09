using BookShop.Services.Models.Categories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookShop.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryListingServiceModel>> All();

        Task<bool> Create(string name);

        Task<CategoryListingServiceModel> Get(int id);

        Task<bool> Delete(int id);

        Task<bool> Edit(int id, string name);
    }
}
