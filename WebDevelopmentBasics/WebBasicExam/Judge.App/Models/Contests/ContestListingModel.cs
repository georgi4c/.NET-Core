using System;
using System.Collections.Generic;
using System.Text;

namespace Judge.App.Models.Contests
{
    public class ContestListingModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int UserId { get; set; }

        public int Submissions { get; set; }
    }
}
