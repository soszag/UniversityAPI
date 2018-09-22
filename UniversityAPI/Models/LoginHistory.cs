using System;
using System.Collections.Generic;

namespace UniversityAPI.Models
{
    public partial class LoginHistory
    {
        public int LoginHistoryId { get; set; }
        public int UserId { get; set; }
        public DateTime LogIn { get; set; }
        public DateTime? LogOutDate { get; set; }
        public string LogoutReason { get; set; }

        public Users User { get; set; }
    }
}
