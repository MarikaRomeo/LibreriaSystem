using System;
using System.Collections.Generic;

namespace LibrarySystem.Core;

public class Book
{
    public int Id { get; set; }                     // Primary key

    public string ISBN { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string Author { get; set; } = string.Empty;

    public int PublishedYear { get; set; }

    public bool IsAvailable { get; set; } = true;

    // Navigation property: un libro può avere molti prestiti
    public ICollection<Loan> Loans { get; set; } = new List<Loan>();
}
