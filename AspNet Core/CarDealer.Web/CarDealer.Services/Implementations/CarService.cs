using System.Collections.Generic;
using CarDealer.Services.Models;
using CarDealer.Data;
using System.Linq;
using CarDealer.Data.Models;

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

        public void Create(string make, string model, long travelledDistance, IEnumerable<int> parts)
        {
            var existingPartIds = this.db
                .Parts
                .Where(p => parts.Contains(p.Id))
                .Select(p => p.Id)
                .ToList();

            var car = new Car()
            {
                Make = make,
                Model = model,
                TravelledDistance = travelledDistance
            };

            foreach (var partId in existingPartIds)
            {
                car.Parts.Add(new PartCar { PartId = partId });
            };

            this.db.Cars.Add(car);
            this.db.SaveChanges();
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
