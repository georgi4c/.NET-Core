using CarDealer.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealer.Services
{
    public interface ICarService
    {
        List<CarModel> ByMake(string make);

        void Create(string make, string model, long travelledDistance, IEnumerable<int> parts);

        List<CarWithParts> WithParts();
    }
}
