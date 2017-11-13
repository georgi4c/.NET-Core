using Judge.App.Infrastructure.Validation;
using Judge.App.Infrastructure.Validation.Contests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Judge.App.Models.Contests
{
    public class ContestModel
    {
        [ContestName, Required]
        public string Name { get; set; }
    }
}
