namespace BookShop.Services.Models.Books
{
    using Common.Mapping;
    using Services.Models.Authors;
    using Data.Models;
    using AutoMapper;
    using System.Linq;

    public class BookDetailsServiceModel : BooksWithCategoriesServiceModel, IMapFrom<Book>, IHaveCustomMapping
    {
        public string Author { get; set; }

        public override void ConfigureMapping(Profile mapper)
            => mapper
                .CreateMap<Book, BookDetailsServiceModel>()
                .ForMember(ba => ba.Categories, cfg => cfg.MapFrom(b => b.Categories.Select(c => c.Category.Name)))
                .ForMember(ba => ba.Author, cfg => cfg.MapFrom(b => b.Author.FirstName + " " + b.Author.LastName));
    }
}
