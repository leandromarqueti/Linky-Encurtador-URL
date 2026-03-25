using Linky.Domain.Entities;

namespace Linky.Domain.Interfaces;

public interface IUrlRepository
{
    Task<UrlRecord?> GetByShortCodeAsync(string shortCode, CancellationToken cancellationToken = default);
    Task<bool> IsShortCodeUniqueAsync(string shortCode, CancellationToken cancellationToken = default);
    Task AddAsync(UrlRecord urlRecord, CancellationToken cancellationToken = default);
    Task UpdateAsync(UrlRecord urlRecord, CancellationToken cancellationToken = default);
}
