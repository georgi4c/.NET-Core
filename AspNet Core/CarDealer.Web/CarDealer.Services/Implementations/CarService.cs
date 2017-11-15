using System.Collections.Generic;
using CarDealer.Services.Models;
using CarDealer.Data;
using System.Linq;

namespace CarDealer.Services
{
    public class CarService : ICarService
    {
        private readonly CarDealerDbContext db;
        public CarService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public List<CarModel> ByMake(string make)
        {
            return db.Cars
                .Where(m => m.Make.ToLower() == make.ToLower())
                .Select(c => new CarModel
                {
                    Model = c.Model,
                    Make = c.Make,
                    TravelledDistance = c.TravelledDistance
                })
                .OrderBy(m => m.Model)
                .ThenByDescending(c => c.TravelledDistance)
                .ToList();
        }

        public List<CarWithParts> WithParts()
        {
            return db.Cars
                .OrderByDescending(c => c.Id)
                .Select(c => new CarWithParts
                {
                Model = c.Model,
                Make = c.Make,
                TravelledDistance = c.TravelledDistance,
                Parts = c.Parts.Select(p => new PartModel
                {
                    Name = p.Part.Name,
                    Price = p.Part.Price
                })
                .ToList()
            })
            .ToList();
        }
    }
}
