using FinTrack.Domain.Entities;
using FinTrack.Domain.Interfaces;
using FinTrack.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FinTrack.Persistence.Repositories
{
    internal class EFCurrencyRepository: ICurrencyRepository
    {
        private readonly FinTrackDbContext _context;

        public EFCurrencyRepository(FinTrackDbContext context) 
        {
            _context = context;
        }

        public async Task AddCurrency(Currency currency)
        {
            _context.Currencies.Add(currency);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Exists(Expression<Func<Currency, bool>> predicate)
        {
            return await _context.Currencies.AnyAsync(predicate);
        }
    }
}
