using MediatR;

namespace Linky.Application.Urls.Commands;

public record CreateUrlShortCommand(string OriginalUrl, int ExpirationDays) : IRequest<string>;
