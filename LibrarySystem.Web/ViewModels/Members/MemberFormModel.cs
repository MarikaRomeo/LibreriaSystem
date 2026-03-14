using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Web.ViewModels.Members;

public class MemberFormModel
{
    public int Id { get; set; }

    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    public DateTime MembershipDate { get; set; } = DateTime.Today;
}
