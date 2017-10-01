using System;

namespace ManyToManyRelation
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new ApplicationDbContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}
