using ModPanel.App.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModPanel.App.Models.Admin
{
    public class AdminUserModel
    {
        public int Id { get; set; }
        
        public string Email { get; set; }
        
        public string Password { get; set; }
        
        public bool IsApproved { get; set; }

        public Position Position { get; set; }
    }
}
