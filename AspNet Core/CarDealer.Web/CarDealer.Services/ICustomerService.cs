﻿using CarDealer.Services.Models;
using System;
using System.Collections.Generic;

namespace CarDealer.Services
{
    public interface ICustomerService
    {
        IEnumerable<CustomerModel> Ordered(OrderDirection order);

        CustomerExpensesModel Expenses(int id);

        void Create(string name, DateTime birthday, bool isYoungDriver);

        void Edit(int id, string name, DateTime birthDate, bool isYoungDriver);

        CustomerModel ById(int id);

        bool Exists(int id);
    }
}
