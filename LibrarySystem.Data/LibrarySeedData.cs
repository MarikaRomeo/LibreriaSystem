using LibrarySystem.Core;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Data;

public static class LibrarySeedData
{
    public static async Task SeedAsync(LibraryContext context)
    {
        if (await context.Books.AnyAsync() || await context.Members.AnyAsync() || await context.Loans.AnyAsync())
        {
            return;
        }

        var books = new List<Book>
        {
            new() { ISBN = "9780140449136", Title = "The Odyssey", Author = "Homer", PublishedYear = -700, IsAvailable = true },
            new() { ISBN = "9780141439518", Title = "Jane Eyre", Author = "Charlotte Bronte", PublishedYear = 1847, IsAvailable = true },
            new() { ISBN = "9780141182803", Title = "Nineteen Eighty-Four", Author = "George Orwell", PublishedYear = 1949, IsAvailable = false },
            new() { ISBN = "9780743273565", Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", PublishedYear = 1925, IsAvailable = true },
            new() { ISBN = "9780553380163", Title = "A Brief History of Time", Author = "Stephen Hawking", PublishedYear = 1988, IsAvailable = false },
            new() { ISBN = "9780307474278", Title = "The Road", Author = "Cormac McCarthy", PublishedYear = 2006, IsAvailable = true },
            new() { ISBN = "9780061120084", Title = "To Kill a Mockingbird", Author = "Harper Lee", PublishedYear = 1960, IsAvailable = true },
            new() { ISBN = "9780307949486", Title = "The Martian", Author = "Andy Weir", PublishedYear = 2011, IsAvailable = false }
        };

        var members = new List<Member>
        {
            new() { FirstName = "Emma", LastName = "Lindberg", Email = "emma.lindberg@example.com", MembershipDate = new DateTime(2024, 2, 12) },
            new() { FirstName = "Johan", LastName = "Eriksson", Email = "johan.eriksson@example.com", MembershipDate = new DateTime(2023, 9, 3) },
            new() { FirstName = "Sara", LastName = "Nilsson", Email = "sara.nilsson@example.com", MembershipDate = new DateTime(2024, 6, 18) },
            new() { FirstName = "Lukas", LastName = "Berg", Email = "lukas.berg@example.com", MembershipDate = new DateTime(2025, 1, 8) },
            new() { FirstName = "Maja", LastName = "Holm", Email = "maja.holm@example.com", MembershipDate = new DateTime(2023, 11, 21) },
            new() { FirstName = "Oskar", LastName = "Svensson", Email = "oskar.svensson@example.com", MembershipDate = new DateTime(2025, 2, 14) }
        };

        await context.Books.AddRangeAsync(books);
        await context.Members.AddRangeAsync(members);
        await context.SaveChangesAsync();

        var loans = new List<Loan>
        {
            new() { BookId = books[2].Id, MemberId = members[0].Id, LoanDate = new DateTime(2026, 3, 1), ReturnDate = null },
            new() { BookId = books[4].Id, MemberId = members[2].Id, LoanDate = new DateTime(2026, 2, 24), ReturnDate = null },
            new() { BookId = books[7].Id, MemberId = members[5].Id, LoanDate = new DateTime(2026, 3, 5), ReturnDate = null },
            new() { BookId = books[1].Id, MemberId = members[1].Id, LoanDate = new DateTime(2026, 1, 9), ReturnDate = new DateTime(2026, 1, 23) },
            new() { BookId = books[6].Id, MemberId = members[4].Id, LoanDate = new DateTime(2025, 12, 14), ReturnDate = new DateTime(2026, 1, 4) }
        };

        await context.Loans.AddRangeAsync(loans);
        await context.SaveChangesAsync();
    }
}
