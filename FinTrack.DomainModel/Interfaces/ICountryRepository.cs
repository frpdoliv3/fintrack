using FinTrack.Domain.Entities;
using System.Linq.Expressions;

namespace FinTrack.Domain.Interfaces
{
    public interface ICountryRepository
    {
        public Task AddCountry(Country country);
        public IAsyncEnumerable<Country> ListCountries(int pageNumber, int pageSize);
        public Task<bool> Exists(Expression<Func<Country, bool>> predicate);
    }
}
