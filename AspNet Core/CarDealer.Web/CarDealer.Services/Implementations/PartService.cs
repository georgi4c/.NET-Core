using System;
using System.Collections.Generic;
using System.Text;
using CarDealer.Services.Models.Parts;
using CarDealer.Data;
using System.Linq;

namespace CarDealer.Services.Implementations
{
    public class PartService : IPartService
    {

        private readonly CarDealerDbContext db;

        public PartService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<PartListingModel> All(int page = 1, int pageSize = 10)
            => this.db
                .Parts
                .OrderByDescending(p => p.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new PartListingModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Quantity = p.Quantity,
                    SupplierName = p.Supplier.Name
                })
                .ToList();

        public int Total()
            => this.db.Parts.Count();
    }
}
