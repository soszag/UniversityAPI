using System;
using System.Collections.Generic;

namespace UniversityAPI.Models
{
    public partial class Users
    {
        public Users()
        {
            LoginHistory = new HashSet<LoginHistory>();
            UsersClaims = new HashSet<UsersClaims>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int? TeacherId { get; set; }
        public int? StudentId { get; set; }
        public bool? IsLogged { get; set; }
        public string SessionId { get; set; }
        public DateTime? LoggedTime { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public int? ModifiedBy { get; set; }
        public int? ParentId { get; set; }

        public Parents Parent { get; set; }
        public Students Student { get; set; }
        public Teachers Teacher { get; set; }
        public ICollection<LoginHistory> LoginHistory { get; set; }
        public ICollection<UsersClaims> UsersClaims { get; set; }
    }
}
