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
        // Arrange
        using var context = GetInMemoryContext();
        var repository = new BookRepository(context);

        context.Books.AddRange(
            new Book { ISBN = "111", Title = "Book 1", Author = "A", PublishedYear = 2020 },
            new Book { ISBN = "222", Title = "Book 2", Author = "B", PublishedYear = 2021 }
        );
        await context.SaveChangesAsync();

        // Act
        var books = await repository.GetAllAsync();

        // Assert
        Assert.Equal(2, books.Count());
    }

    [Fact]
    public async Task GetByISBNAsync_ShouldReturnCorrectBook()
    {
        // Arrange
        using var context = GetInMemoryContext();
        var repository = new BookRepository(context);

        var book = new Book { ISBN = "999", Title = "Special Book", Author = "X", PublishedYear = 2022 };
        await repository.AddAsync(book);

        // Act
        var result = await repository.GetByISBNAsync("999");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Special Book", result!.Title);
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveBook()
    {
        // Arrange
        using var context = GetInMemoryContext();
        var repository = new BookRepository(context);

        var book = new Book { ISBN = "555", Title = "To Delete", Author = "Y", PublishedYear = 2023 };
        await repository.AddAsync(book);

        // Act
        await repository.DeleteAsync(book.Id);

        // Assert
        var deletedBook = await context.Books.FindAsync(book.Id);
        Assert.Null(deletedBook);
    }

    [Fact]
    public async Task SearchAsync_ShouldFindBooksByTitleOrAuthor()
    {
        // Arrange
        using var context = GetInMemoryContext();
        var repository = new BookRepository(context);

        context.Books.AddRange(
            new Book { ISBN = "111", Title = "C# Basics", Author = "Alice", PublishedYear = 2020 },
            new Book { ISBN = "222", Title = "Advanced C#", Author = "Bob", PublishedYear = 2021 },
            new Book { ISBN = "333", Title = "Python Intro", Author = "Charlie", PublishedYear = 2022 }
        );
        await context.SaveChangesAsync();

        // Act
        var result = await repository.SearchAsync("C#");

        // Assert
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnCorrectBook()
    {
        // Arrange
        using var context = GetInMemoryContext();
        var repository = new BookRepository(context);

        var book = new Book
        {
            ISBN = "444",
            Title = "Domain-Driven Design",
            Author = "Eric Evans",
            PublishedYear = 2003
        };

        await repository.AddAsync(book);

        // Act
        var result = await repository.GetByIdAsync(book.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Domain-Driven Design", result!.Title);
    }

    [Fact]
    public async Task UpdateAsync_ShouldModifyExistingBook()
    {
        // Arrange
        using var context = GetInMemoryContext();
        var repository = new BookRepository(context);

        var book = new Book
        {
            ISBN = "777",
            Title = "Original Title",
            Author = "Original Author",
            PublishedYear = 2020
        };

        await repository.AddAsync(book);

        book.Title = "Updated Title";
        book.Author = "Updated Author";

        // Act
        await repository.UpdateAsync(book);

        // Assert
        var updatedBook = await context.Books.FindAsync(book.Id);
        Assert.NotNull(updatedBook);
        Assert.Equal("Updated Title", updatedBook!.Title);
        Assert.Equal("Updated Author", updatedBook.Author);
    }

    [Fact]
    public async Task DeleteAsync_WithMissingId_ShouldNotThrowAndShouldKeepOtherBooks()
    {
        // Arrange
        using var context = GetInMemoryContext();
        var repository = new BookRepository(context);

        await repository.AddAsync(new Book
        {
            ISBN = "888",
            Title = "Keep Me",
            Author = "Author",
            PublishedYear = 2021
        });

        // Act
        await repository.DeleteAsync(9999);

        // Assert
        Assert.Single(context.Books);
    }

    [Fact]
    public async Task SearchAsync_ShouldReturnEmptyCollection_WhenNoBookMatches()
    {
        // Arrange
        using var context = GetInMemoryContext();
        var repository = new BookRepository(context);

        context.Books.AddRange(
            new Book { ISBN = "101", Title = "Clean Code", Author = "Robert Martin", PublishedYear = 2008 },
            new Book { ISBN = "202", Title = "Refactoring", Author = "Martin Fowler", PublishedYear = 1999 }
        );
        await context.SaveChangesAsync();

        // Act
        var result = await repository.SearchAsync("Rust");

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task AddAsync_ThenGetAllAsync_ShouldIncludeNewBook()
    {
        // Arrange
        using var context = GetInMemoryContext();
        var repository = new BookRepository(context);

        await repository.AddAsync(new Book
        {
            ISBN = "303",
            Title = "Pragmatic Programmer",
            Author = "Andrew Hunt",
            PublishedYear = 1999
        });

        // Act
        var books = await repository.GetAllAsync();

        // Assert
        Assert.Contains(books, book => book.ISBN == "303" && book.Title == "Pragmatic Programmer");
    }
}
