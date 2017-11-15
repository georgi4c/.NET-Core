using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarDealer.Web.Models.Customers
{
    public class CustomerFormModel
    {
        [Required]
        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        [Display(Name = "Young Driver")]
        public bool IsYoungDriver { get; set; }
    }
}
