using System;
using System.Collections.Generic;
using System.Text;
using CarDealer.Services.Models;
using CarDealer.Data;
using System.Linq;

namespace CarDealer.Services.Implementations
{
    public class SaleService : ISaleService
    {
        private CarDealerDbContext db;
        public SaleService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<SaleListModel> All()
            => this.db
            .Sales
            .Select(s => new SaleListModel
            {
                Id = s.Id,
                CustomerName = s.Customer.Name,
                Price = s.Car.Parts.Sum(p => p.Part.Price),
                IsYoungDriver = s.Customer.IsYoungDriver,
                Discount = s.Discount
            })
            .ToList();

        public SaleDetailsModel Details(int id)
            => this.db
            .Sales
            .Where(s => s.Id == id)
            .Select(s => new SaleDetailsModel()
            {
                Id = s.Id,
                CustomerName = s.Customer.Name,
                Price = s.Car.Parts.Sum(p => p.Part.Price),
                IsYoungDriver = s.Customer.IsYoungDriver,
                Discount = s.Discount,
                Car = new CarModel()
                {
                    Make = s.Car.Make,
                    Model = s.Car.Model,
                    TravelledDistance = s.Car.TravelledDistance
                }
            })
            .FirstOrDefault();
    }
}
