using System;
using System.Linq;

namespace ShopHierarchy
{
    public class Program
    {
        public static void Main()
        {
            var db = new ShopHierarchyContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            AddSalesMenByNames(db);
            AddItems(db);
            ReadCommands(db);
            //PrintSalesmenInfo(db);
            //PrintCustomerInfo(db);
            //PrintInfoForSpecificCustomer(db);
            //PrintExplicitDataLoadingCustomerInfo(db);
            PrintQueryLoadedDataCustomerInfo(db);
        }

        private static void PrintQueryLoadedDataCustomerInfo(ShopHierarchyContext db)
        {
            var targetCustomerId = int.Parse(Console.ReadLine());

            var customerData = db.Customers
                .Where(c => c.Id == targetCustomerId)
                .Select(c => new
                {
                    Orders = c.Orders.Where(o => o.Items.Count > 1).Count()
                })
                .FirstOrDefault();

            Console.WriteLine($"Orders: {customerData.Orders}");
        }

        private static void PrintExplicitDataLoadingCustomerInfo(ShopHierarchyContext db)
        {
            var targetCustomerId = int.Parse(Console.ReadLine());

            var customerData = db.Customers
                .Where(c => c.Id == targetCustomerId)
                .Select(c => new
                {
                    c.Name,
                    Orders = c.Orders.Count,
                    Reviews = c.Reviews.Count,
                    Salesman = c.Salesman.Name
                })
                .FirstOrDefault();

            Console.WriteLine($"Customer: {customerData.Name}");
            Console.WriteLine($"Orders count: {customerData.Orders}");
            Console.WriteLine($"Reviews count: {customerData.Reviews}");
            Console.WriteLine($"Salesman: {customerData.Salesman}");
        }

        private static void PrintInfoForSpecificCustomer(ShopHierarchyContext db)
        {
            var targetCustomerId = int.Parse(Console.ReadLine());

            var customerData = db.Customers
                .Where(c => c.Id == targetCustomerId)
                .Select(c => new
                {
                    Reviews = c.Reviews.Count,
                    Orders = c.Orders
                        .Select(o => new
                        {
                            Id = o.Id,
                            Items = o.Items.Count
                        })
                        .OrderBy(o => o.Id)
                })
                .FirstOrDefault();

            foreach (var order in customerData.Orders)
            {
                Console.WriteLine($"order {order.Id}: {order.Items} items");
            }

            Console.WriteLine($"reviews: {customerData.Reviews}");
        }

        private static void AddItems(ShopHierarchyContext db)
        {
            while (true)
            {
                var inputLine = Console.ReadLine();
                if (inputLine == "END")
                {
                    break;
                }

                var itemArgs = inputLine.Split(';');
                var itemName = itemArgs[0];
                var itemPrice = decimal.Parse(itemArgs[1]);

                db.Add(new Item { Name = itemName, Price = itemPrice });
            }

            db.SaveChanges();
        }

        private static void PrintCustomerInfo(ShopHierarchyContext db)
        {
            var customers = db.Customers
                .Select(c => new
                {
                    Name = c.Name,
                    Orders = c.Orders.Count,
                    Reviews = c.Reviews.Count
                })
                .OrderByDescending(c => c.Orders)
                .ThenByDescending(c => c.Reviews)
                .ToList();

            foreach (var customer in customers)
            {
                Console.WriteLine(customer.Name);
                Console.WriteLine($"Orders: {customer.Orders}");
                Console.WriteLine($"Reviews: {customer.Reviews}");
            }
        }

        private static void PrintSalesmenInfo(ShopHierarchyContext db)
        {
            var salesmen = db.Salesmen
                .Select(s => new
                {
                    Name = s.Name,
                    Customers = s.Customers.Count
                })
                .OrderByDescending(s => s.Customers)
                .ThenBy(s => s.Name)
                .ToList();

            foreach (var salesman in salesmen)
            {
                Console.WriteLine($"{salesman.Name} - {salesman.Customers} customers");
            }
        }

        private static void ReadCommands(ShopHierarchyContext db)
        {
            while (true)
            {
                var line = Console.ReadLine();
                if (line == "END")
                {
                    break;
                }

                var commandArgs = line.Split('-');
                var commandName = commandArgs[0];
                var commandData = commandArgs[1];

                switch (commandName)
                {
                    case "register":
                        AddCustomer(db, commandData);
                        break;
                    case "order":
                        AddOrder(db, commandData);
                        break;
                    case "review":
                        AddReview(db, commandData);
                        break;
                    default:
                        break;
                }
            }

            db.SaveChanges();
        }

        private static void AddReview(ShopHierarchyContext db, string commandData)
        {
            var orderDataArgs = commandData.Split(';');
            var customerId = int.Parse(orderDataArgs[0]);
            var itemId = int.Parse(orderDataArgs[1]);
            db.Reviews.Add(new Review { CustomerId = customerId, ItemId = itemId });
        }

        private static void AddOrder(ShopHierarchyContext db, string commandData)
        {
            var orderDataArgs = commandData.Split(';');
            var customerId = int.Parse(orderDataArgs[0]);
            var order = new Order { CustomerId = customerId };

            for (int i = 1; i < orderDataArgs.Length; i++)
            {
                var itemId = int.Parse(orderDataArgs[i]);
                order.Items.Add(new OrderItem { ItemId = itemId });
            }

            db.Orders.Add(order);
        }

        private static void AddCustomer(ShopHierarchyContext db, string commandData)
        {
            var customerDataArgs = commandData.Split(';');
            var customerName = customerDataArgs[0];
            var salesmanId = int.Parse(customerDataArgs[1]);
            db.Customers.Add(new Customer { Name = customerName, SalesmanId = salesmanId });
        }

        private static void AddSalesMenByNames(ShopHierarchyContext db)
        {
            var salesmenNames = Console.ReadLine().Split(';');

            foreach (var salesmanName in salesmenNames)
            {
                db.Salesmen.Add(new Salesman { Name = salesmanName });
            }

            db.SaveChanges();
        }
    }
}
