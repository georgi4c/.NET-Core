using System.Collections.Generic;
using CarDealer.Data;
using CarDealer.Services.Models;
using System;
using System.Linq;
using CarDealer.Services.Models.Suppliers;

namespace CarDealer.Services
{
    public class SupplierService : ISupplierService
    {
        private CarDealerDbContext db;

        public SupplierService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<SupplierModel> All()
            => this.db
                .Suppliers
            .OrderBy(s => s.Name)
            .Select(s => new SupplierModel
            {
                Id = s.Id,
                Name = s.Name
            })
            .ToList();

        public List<SupplierListingModel> AllListings(bool isImporter)
        {
            return db.Suppliers
                .OrderByDescending(s => s.Id)
                .Where(s => s.IsImporter == isImporter)
                .Select(s => new SupplierListingModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Parts = s.Parts.Count
                })
                .ToList();
        }
    }
}
