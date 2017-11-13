using System.Collections.Generic;
using CarDealer.Data;
using CarDealer.Services.Models;
using System;
using System.Linq;

namespace CarDealer.Services
{
    public class SupplierService : ISupplierService
    {
        private CarDealerDbContext db;

        public SupplierService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public List<SupplierModel> All(bool isImporter)
        {
            return db.Suppliers
                .Where(s => s.IsImporter == isImporter)
                .Select(s => new SupplierModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Parts = s.Parts.Count
                })
                .ToList();
        }
    }
}
