using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookShop.Services.Models.Categories;
using BookShop.Data;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using BookShop.Data.Models;

namespace BookShop.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly BookShopDbContext db;

        public CategoryService(BookShopDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<CategoryListingServiceModel>> All()
            => await this.db
                .Categories
                .ProjectTo<CategoryListingServiceModel>()
                .ToListAsync();

        public async Task<bool> Create(string name)
        {
            if (await this.DoesCategoryExist(name))
            {
                return false;
            }

            var category = new Category
            {
                Name = name
            };

            this.db.Add(category);
            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var category = this.db.Categories.Find(id);
            if (category == null)
            {
                return false;
            }

            this.db.Remove(category);
            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Edit(int id, string name)
        {
            if (await this.DoesCategoryExist(name))
            {
                return false;
            }

            var category = this.db.Categories.Find(id);
            if (category == null)
            {
                return false;
            }

            category.Name = name;
            await this.db.SaveChangesAsync();

            return true;
        }

        public Task<CategoryListingServiceModel> Get(int id)
            => this.db
                .Categories
                .Where(c => c.Id == id)
                .ProjectTo<CategoryListingServiceModel>()
                .FirstOrDefaultAsync();

        private async Task<bool> DoesCategoryExist(string name)
            => await this.db
                .Categories
                .AnyAsync(c => c.Name == name);
    }
}
