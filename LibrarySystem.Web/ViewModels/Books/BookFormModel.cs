using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Web.ViewModels.Books;

public class BookFormModel
{
    public int Id { get; set; }

    [Required]
    public string ISBN { get; set; } = string.Empty;

    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Author { get; set; } = string.Empty;

    [Range(0, 3000)]
    public int PublishedYear { get; set; } = DateTime.Today.Year;

    public bool IsAvailable { get; set; } = true;
}
