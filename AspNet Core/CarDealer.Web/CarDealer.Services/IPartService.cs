using CarDealer.Services.Models.Parts;
using System.Collections.Generic;

namespace CarDealer.Services
{
    public interface IPartService
    {
        IEnumerable<PartListingModel> All(int page = 1, int pageSize = 10);

        int Total();
    }
}
