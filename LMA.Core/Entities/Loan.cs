namespace LMA.Core.Entities;

public class Loan : Base
{
    public int BorrowerId { get; set; }
    public int BookId { get; set; }
    public DateTime LoanDate { get;  set; }
    public DateTime MustReturnDate { get;  set; }
    public DateTime? ReturnDate { get; set; }
    public Borrower Borrower { get; set; }
    public Book Book { get; set; }

}
