using BookShop.Services.Models.Books;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookShop.Services
{
    public interface IBookService
    {
        Task<IEnumerable<BookListingServiceModel>> All(string searchTerm);

        Task<int> Create(
            string title,
            string description,
            decimal price,
            int copies,
            int? edition,
            int? ageRestriction,
            DateTime releaseDate,
            int authorId,
            string categories);

        Task<BookDetailsServiceModel> Details(int id);

        Task<bool> Edit(
            int bookId,
            string title,
            string description,
            decimal price,
            int copies,
            int? edition,
            int? ageRestriction,
            DateTime releaseDate,
            int authorId,
            string categories);

        Task<bool> Exists(int id);

        Task<bool> Delete(int id);
    }
}
