using Linky.Domain.Entities;
using Linky.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Linky.Infrastructure.Persistence.Repositories;

public class UrlRepository : IUrlRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UrlRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(UrlRecord urlRecord, CancellationToken cancellationToken = default)
    {
        await _dbContext.UrlRecords.AddAsync(urlRecord, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<UrlRecord?> GetByShortCodeAsync(string shortCode, CancellationToken cancellationToken = default)
    {
        return await _dbContext.UrlRecords
            .FirstOrDefaultAsync(u => u.ShortCode == shortCode, cancellationToken);
    }

    public async Task<bool> IsShortCodeUniqueAsync(string shortCode, CancellationToken cancellationToken = default)
    {
        return !await _dbContext.UrlRecords.AnyAsync(u => u.ShortCode == shortCode, cancellationToken);
    }

    public async Task UpdateAsync(UrlRecord urlRecord, CancellationToken cancellationToken = default)
    {
        _dbContext.UrlRecords.Update(urlRecord);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
