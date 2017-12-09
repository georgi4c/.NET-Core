using BookShop.Services.Models;
using BookShop.Services.Models.Authors;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookShop.Services
{
    public interface IAuthorService
    {
        Task<IEnumerable<BooksWithCategoriesServiceModel>> Books(int authorId);

        Task<int> Create(string firstName, string lastName);

        Task<AuthorDetailsServiceModel> Details(int id);

        Task<bool> Exists(int id);
    }
}
