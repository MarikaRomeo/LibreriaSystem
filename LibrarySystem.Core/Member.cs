using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.Core
{
    public class Member
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public DateTime MembershipDate { get; set; } = DateTime.Now;

        // Navigation property
        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
    }

}
