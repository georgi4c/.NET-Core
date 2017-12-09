using System;
using System.Threading.Tasks;
using BookShop.Services.Models.Books;
using BookShop.Data;
using System.Linq;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using BookShop.Data.Models;
using BookShop.Common.Extensions;

namespace BookShop.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly BookShopDbContext db;

        public BookService(BookShopDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<BookListingServiceModel>> All(string searchTerm)
            => await this.db
                .Books
                .Where(b => b.Title.ToLower().Contains(searchTerm.ToLower()))
                .OrderBy(b => b.Title)
                .Take(10)
                .ProjectTo<BookListingServiceModel>()
                .ToListAsync();

        public async Task<int> Create(string title, string description, decimal price, int copies, int? edition, int? ageRestriction, DateTime releaseDate, int authorId, string categories)
        {
            var allCategories = await this.ParseCategories(categories);

            var book = new Book
            {
                Title = title,
                Description = description,
                Price = price,
                Copies = copies,
                Edition = edition,
                AgeRestriction = ageRestriction,
                ReleaseDate = releaseDate,
                AuthorId = authorId
            };

            allCategories.ForEach(c => book.Categories.Add(new BookCategory
            {
                CategoryId = c.Id
            }));

            this.db.Add(book);
            await this.db.SaveChangesAsync();

            return book.Id;
        }

        public async Task<bool> Delete(int id)
        {
            var book = await this.db.Books.FindAsync(id);

            if (book == null)
            {
                return false;
            }

            var categoriesToDelete = this.db.BookCategories.Where(bc => bc.BookId == id);
            this.db.RemoveRange(categoriesToDelete);
            this.db.Remove(book);
            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<BookDetailsServiceModel> Details(int id)
            => await this.db
                .Books
                .Where(b => b.Id == id)
                .ProjectTo<BookDetailsServiceModel>()
                .FirstOrDefaultAsync();

        public async Task<bool> Edit(int bookId, string title, string description, decimal price, int copies, int? edition, int? ageRestriction, DateTime releaseDate, int authorId, string categories)
        {
            var allCategories = await this.ParseCategories(categories);

            var book = await this.db.Books.FirstOrDefaultAsync(b => b.Id == bookId);

            if (book == null)
            {
                return false;
            }
            
            book.Title = title;
            book.Description = description;
            book.Price = price;
            book.Copies = copies;
            book.Edition = edition;
            book.AgeRestriction = ageRestriction;
            book.ReleaseDate = releaseDate;
            book.AuthorId = authorId;

            var oldBookCategories = this.db.BookCategories.Where(bc => bc.BookId == bookId);
            this.db.BookCategories.RemoveRange(oldBookCategories);

            allCategories.ForEach(c => book.Categories.Add(new BookCategory
            {
                CategoryId = c.Id
            }));
            
            await this.db.SaveChangesAsync();

            return true;
        }
        

        public async Task<bool> Exists(int id)
            => await this.db
                .Books
                .AnyAsync(a => a.Id == id);

        private async Task<List<Category>> ParseCategories(string categories)
        {
            if (string.IsNullOrEmpty(categories))
            {
                return new List<Category>();
            }
            var categoryNames = categories
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .ToHashSet();

            var existingCategories = await this.db
                .Categories
                .Where(c => categoryNames.Contains(c.Name))
                .ToListAsync();

            var allCategories = new List<Category>(existingCategories);

            foreach (var categoryName in categoryNames)
            {
                if (existingCategories.All(c => c.Name != categoryName))
                {
                    var category = new Category
                    {
                        Name = categoryName
                    };

                    allCategories.Add(category);
                    this.db.Add(category);
                }
            }

            await this.db.SaveChangesAsync();

            return allCategories;
        }
    }
}
