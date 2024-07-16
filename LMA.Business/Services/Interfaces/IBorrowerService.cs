using LMA.Core.Entities;
namespace LMA.Business.Services.Interfaces;

public interface IBorrowerService
{
    Task<Borrower> GetById(int id);
    Task DeleteAsync(int id);
    Task<List<Borrower>> GetAllAsync();
    Task CreateAsync(Borrower borrower);
    Task UpdateAsync(Borrower borrower);
    List<Borrower> GetLateReturners(List<Borrower> allBorrowers, bool isLate = true);
    Task<List<Borrower>> GetBorrowersWithBorrowedBooks();

}
