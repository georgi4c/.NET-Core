using CarDealer.Services.Models.Parts;
using System.Collections.Generic;

namespace CarDealer.Services
{
    public interface IPartService
    {
        IEnumerable<PartListingModel> AllListings(int page = 1, int pageSize = 10);

        IEnumerable<PartBasicModel> All();

        PartDetailsModel ById(int id);

        void Create(string name, decimal price, int quantity, int supplierId);

        void Delete(int id);

        void Edit(int id, decimal price, int quantity);

        int Total();
    }
}
