using System;
using System.Collections.Generic;

namespace OneToManyRelationship
{
    class Program
    {
        static void Main()
        {
            var context = new ApplicationDbContext();
            context.Database.EnsureCreated();
        }
    }
}
