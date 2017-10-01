using System;

namespace SelfReferenceTable
{
    class Program
    {
        static void Main()
        {
            var context = new ApplicationDbContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}
