using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Web.ViewModels.Loans;

public class LoanFormModel
{
    [Range(1, int.MaxValue, ErrorMessage = "Välj en bok.")]
    public int BookId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Välj en medlem.")]
    public int MemberId { get; set; }

    public DateTime LoanDate { get; set; } = DateTime.Today;
}
