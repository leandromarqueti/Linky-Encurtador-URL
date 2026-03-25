using Linky.Domain.Interfaces;
using MediatR;

namespace Linky.Application.Urls.Queries;

public class GetOriginalUrlQueryHandler : IRequestHandler<GetOriginalUrlQuery, string?>
{
    private readonly IUrlRepository _urlRepository;

    public GetOriginalUrlQueryHandler(IUrlRepository urlRepository)
    {
        _urlRepository = urlRepository;
    }

    public async Task<string?> Handle(GetOriginalUrlQuery request, CancellationToken cancellationToken)
    {
        var urlRecord = await _urlRepository.GetByShortCodeAsync(request.ShortCode, cancellationToken);
        
        if (urlRecord == null || (urlRecord.ExpiresAt != DateTime.MaxValue && urlRecord.ExpiresAt < DateTime.UtcNow))
        {
            return null; // Expired or not found
        }

        urlRecord.AccessCount++;
        await _urlRepository.UpdateAsync(urlRecord, cancellationToken);

        return urlRecord.OriginalUrl;
    }
}
