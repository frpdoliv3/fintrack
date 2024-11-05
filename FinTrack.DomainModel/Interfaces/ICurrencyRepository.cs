using FinTrack.Domain.Entities;
using System.Linq.Expressions;

namespace FinTrack.Domain.Interfaces;

public interface ICurrencyRepository
{
    public Task<bool> Exists(Expression<Func<Currency, bool>> predicate);
    public Task<Currency> AddCurrency(Currency currency);
    public Task<PagedList<Currency>> GetCurrencies(string searchQuery, int pageNumber, int pageSize);
    public Task<Currency?> GetCurrencyById(uint id);
}
