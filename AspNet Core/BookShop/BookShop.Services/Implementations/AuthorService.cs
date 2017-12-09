using BookShop.Data;
using System;
using BookShop.Services.Models.Authors;
using System.Linq;
using AutoMapper.QueryableExtensions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BookShop.Data.Models;
using BookShop.Services.Models;
using System.Collections.Generic;

namespace BookShop.Services.Implementations
{
    public class AuthorService : IAuthorService
    {
        private readonly BookShopDbContext db;

        public AuthorService(BookShopDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<BooksWithCategoriesServiceModel>> Books(int authorId)
            => await this.db
                .Books
                .Where(b => b.AuthorId == authorId)
                .ProjectTo<BooksWithCategoriesServiceModel>()
                .ToListAsync();

        public async Task<int> Create(string firstName, string lastName)
        {
            var author = new Author
            {
                FirstName = firstName,
                LastName = lastName
            };

            this.db.Add(author);
            await this.db.SaveChangesAsync();

            return author.Id;
        }

        public async Task<AuthorDetailsServiceModel> Details(int id)
            => await this.db
            .Authors
            .Where(a => a.Id == id)
            .ProjectTo<AuthorDetailsServiceModel>()
            .FirstOrDefaultAsync();

        public async Task<bool> Exists(int id)
            => await this.db
                .Authors
                .AnyAsync(a => a.Id == id);
    }
}
