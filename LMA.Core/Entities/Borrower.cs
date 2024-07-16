namespace LMA.Core.Entities;

public class Borrower : Base
{
    private bool _lateReturner;
    public string Name { get; set; }
    public string Email { get; set; }
    public bool LateReturner { get; set; }
    public List<Book> Books { get; set; }
    public List <Loan> Loans { get; set; }  


}