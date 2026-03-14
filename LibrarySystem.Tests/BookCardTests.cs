using Bunit;
using LibrarySystem.Core;
using LibrarySystem.Web.Components.Shared;

namespace LibrarySystem.Tests;

public class BookCardTests : TestContext
{
    [Fact]
    public void BookCard_ShouldDisplayBookInformation_ForAvailableBook()
    {
        // Arrange
        var book = new Book
        {
            ISBN = "9780131103627",
            Title = "The C Programming Language",
            Author = "Kernighan and Ritchie",
            PublishedYear = 1988,
            IsAvailable = true
        };

        // Act
        var cut = RenderComponent<BookCard>(parameters => parameters.Add(component => component.Book, book));

        // Assert
        Assert.Contains("The C Programming Language", cut.Markup);
        Assert.Contains("Kernighan and Ritchie", cut.Markup);
        Assert.Contains("9780131103627", cut.Markup);
        Assert.Contains("1988", cut.Markup);
        Assert.Contains("Tillgänglig", cut.Markup);
    }
}
