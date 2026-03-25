using Linky.Domain.Entities;
using Linky.Domain.Interfaces;
using MediatR;

namespace Linky.Application.Urls.Commands;

public class CreateUrlShortCommandHandler : IRequestHandler<CreateUrlShortCommand, string>
{
    private readonly IUrlRepository _urlRepository;
    private static readonly Random Random = new();
    private const string Alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    public CreateUrlShortCommandHandler(IUrlRepository urlRepository)
    {
        _urlRepository = urlRepository;
    }

    public async Task<string> Handle(CreateUrlShortCommand request, CancellationToken cancellationToken)
    {
        string shortCode;
        do
        {
            shortCode = GenerateShortCode(6);
        }
        while (!await _urlRepository.IsShortCodeUniqueAsync(shortCode, cancellationToken));

        var urlRecord = new UrlRecord
        {
            Id = Guid.NewGuid(),
            OriginalUrl = request.OriginalUrl,
            ShortCode = shortCode,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = request.ExpirationDays > 0 ? DateTime.UtcNow.AddDays(request.ExpirationDays) : DateTime.MaxValue,
            AccessCount = 0
        };

        await _urlRepository.AddAsync(urlRecord, cancellationToken);

        return shortCode;
    }

    private string GenerateShortCode(int length)
    {
        var chars = new char[length];
        for (int i = 0; i < length; i++)
        {
            chars[i] = Alphabet[Random.Next(Alphabet.Length)];
        }
        return new string(chars);
    }
}
