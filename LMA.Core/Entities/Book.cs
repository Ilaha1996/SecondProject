namespace LMA.Core.Entities;

public class Book : Base
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int PublishedYear { get; set; }
    public int? BorrowerId { get; set; }

    public bool IsAvailable { get; set; }
    public int? BorrowedTimes{get; set; }   

    public Borrower? Borrower { get; set; }
    public List<Loan> Loans { get; set; }
    public List <BookAuthors> BookAuthors { get; set; }

}
