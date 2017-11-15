using CarDealer.Data;
using CarDealer.Services.Models;
using CarDealer.Services.Models.Suppliers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealer.Services
{
    public interface ISupplierService
    {
        List<SupplierListingModel> AllListings(bool isImporter);

        IEnumerable<SupplierModel> All();
    }
}
