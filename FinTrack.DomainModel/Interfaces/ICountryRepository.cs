using FinTrack.Domain.Entities;
using System.Linq.Expressions;

namespace FinTrack.Domain.Interfaces;

public interface ICountryRepository
{
    public Task<Country> AddCountry(Country country);
    public Task<Country?> GetCountryById(uint id);
    public IAsyncEnumerable<Country> ListCountries(int pageNumber, int pageSize);
    public Task<bool> Exists(Expression<Func<Country, bool>> predicate);
}
