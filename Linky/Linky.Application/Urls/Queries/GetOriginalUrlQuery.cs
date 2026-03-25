using MediatR;

namespace Linky.Application.Urls.Queries;

public record GetOriginalUrlQuery(string ShortCode) : IRequest<string?>;
