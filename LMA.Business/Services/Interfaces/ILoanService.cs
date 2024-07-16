using LMA.Core.Entities;

namespace LMA.Business.Services.Interfaces;

public interface ILoanService
{
    Task<Loan> GetById(int id);
    Task DeleteAsync(int id);
    Task<List<Loan>> GetAllAsync();
    Task CreateAsync(Loan loan);
    Task UpdateAsync(Loan loan);
    Task<List<Loan>> GetAllByBorrowerId(int borrowerId);
}
