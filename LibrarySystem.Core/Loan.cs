using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.Core
{
    public class Loan
    {
        public int Id { get; set; }

        // Foreign key Book
        public int BookId { get; set; }
        public Book Book { get; set; } = null!;

        // Foreign key Member
        public int MemberId { get; set; }
        public Member Member { get; set; } = null!;

        public DateTime LoanDate { get; set; } = DateTime.Now;

        public DateTime? ReturnDate { get; set; }

        public bool IsReturned => ReturnDate != null;
    }

}
