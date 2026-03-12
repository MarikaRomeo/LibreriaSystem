using LibrarySystem.Core;
using LibrarySystem.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace LibrarySystem.Tests;

public class BookRepositoryTests
{
    // Metodo helper: crea un DbContext InMemory
    private LibraryContext GetInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<LibraryContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // DB pulito ogni volta
            .Options;

        return new LibraryContext(options);
    }

    [Fact]
    public async Task AddAsync_ShouldSaveBookToDatabase()
    {
        // Arrange
        using var context = GetInMemoryContext();
        var repository = new BookRepository(context);

        var book = new Book
        {
            ISBN = "123",
            Title = "Test Book",
            Author = "Author",
            PublishedYear = 2024
        };

        // Act
        await repository.AddAsync(book);

        // Assert
        var savedBook = await context.Books.FirstOrDefaultAsync(b => b.ISBN == "123");
        Assert.NotNull(savedBook);
        Assert.Equal("Test Book", savedBook!.Title);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllBooks()
    {
        using var context = GetInMemoryContext();
        var repository = new BookRepository(context);

        context.Books.AddRange(
            new Book { ISBN = "111", Title = "Book 1", Author = "A", PublishedYear = 2020 },
            new Book { ISBN = "222", Title = "Book 2", Author = "B", PublishedYear = 2021 }
        );
        await context.SaveChangesAsync();

        var books = await repository.GetAllAsync();

        Assert.Equal(2, books.Count());
    }

    [Fact]
    public async Task GetByISBNAsync_ShouldReturnCorrectBook()
    {
        using var context = GetInMemoryContext();
        var repository = new BookRepository(context);

        var book = new Book { ISBN = "999", Title = "Special Book", Author = "X", PublishedYear = 2022 };
        await repository.AddAsync(book);

        var result = await repository.GetByISBNAsync("999");

        Assert.NotNull(result);
        Assert.Equal("Special Book", result!.Title);
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveBook()
    {
        using var context = GetInMemoryContext();
        var repository = new BookRepository(context);

        var book = new Book { ISBN = "555", Title = "To Delete", Author = "Y", PublishedYear = 2023 };
        await repository.AddAsync(book);

        await repository.DeleteAsync(book.Id);

        var deletedBook = await context.Books.FindAsync(book.Id);
        Assert.Null(deletedBook);
    }

    [Fact]
    public async Task SearchAsync_ShouldFindBooksByTitleOrAuthor()
    {
        using var context = GetInMemoryContext();
        var repository = new BookRepository(context);

        context.Books.AddRange(
            new Book { ISBN = "111", Title = "C# Basics", Author = "Alice", PublishedYear = 2020 },
            new Book { ISBN = "222", Title = "Advanced C#", Author = "Bob", PublishedYear = 2021 },
            new Book { ISBN = "333", Title = "Python Intro", Author = "Charlie", PublishedYear = 2022 }
        );
        await context.SaveChangesAsync();

        var result = await repository.SearchAsync("C#");

        Assert.Equal(2, result.Count());
    }
}
