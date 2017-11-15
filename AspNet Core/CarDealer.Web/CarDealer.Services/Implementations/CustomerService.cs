using CarDealer.Data;
using System;
using System.Collections.Generic;
using CarDealer.Services.Models;
using System.Linq;
using CarDealer.Data.Models;

namespace CarDealer.Services.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly CarDealerDbContext db;
        public CustomerService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public void Create(string name, DateTime birthday, bool isYoungDriver)
        {
            var customer = new Customer()
            {
                Name = name,
                BirthDate = birthday,
                IsYoungDriver = isYoungDriver
            };

            this.db.Add(customer);
            this.db.SaveChanges();
        }

        public CustomerExpensesModel Expenses(int id)
        {
            return db.Customers.Where(c => c.Id == id).Select(c =>
                new CustomerExpensesModel()
            {
                Name = c.Name,
                BoughtCars = c.Sales.Count,
                SpentMoney = c.Sales.Sum(s => s.Car.Parts.Sum(p => p.Part.Price))
            })
            .FirstOrDefault();
        }

        public IEnumerable<CustomerModel> Ordered(OrderDirection order)
        {
            var customersQuery = this.db.Customers.AsQueryable();

            switch (order)
            {
                case OrderDirection.Ascending:
                    customersQuery = customersQuery
                        .OrderBy(c => c.BirthDate)
                        .ThenBy(c => c.IsYoungDriver);
                    break;
                case OrderDirection.Descending:
                    customersQuery = customersQuery
                        .OrderByDescending(c => c.BirthDate)
                        .ThenBy(c => c.IsYoungDriver);
                    break;
                default:
                    throw new InvalidOperationException($"Invalid order directionL {order}");
            }
            return customersQuery
                .Select(c => new CustomerModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    BirthDate = c.BirthDate,
                    IsYoungDriver = c.IsYoungDriver
                })
                .ToList();
        }

        public CustomerModel ById(int id)
            => this.db
                .Customers
                .Where(c => c.Id == id)
                .Select(c => new CustomerModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    BirthDate = c.BirthDate,
                    IsYoungDriver = c.IsYoungDriver
                })
                .FirstOrDefault();

        public void Edit(int id, string name, DateTime birthDate, bool isYoungDriver)
        {
            var existingCustomer = this.db.Customers.Find(id);

            if (existingCustomer == null)
            {
                return;
            }

            existingCustomer.Name = name;
            existingCustomer.BirthDate = birthDate;
            existingCustomer.IsYoungDriver = isYoungDriver;

            this.db.SaveChanges();
        }

        public bool Exists(int id)
            => this.db.Customers.Any(c => c.Id == id);
    }
}
