using Linky.Application.Urls.Commands;
using Linky.Application.Urls.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Linky.Api.Controllers;

[ApiController]
[Route("")]
public class UrlsController : ControllerBase
{
    private readonly IMediator _mediator;

    public UrlsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("api/urls")]
    public async Task<IActionResult> CreateUrlShort([FromBody] CreateUrlRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.OriginalUrl))
        {
            return BadRequest("OriginalUrl is required.");
        }

        var command = new CreateUrlShortCommand(request.OriginalUrl, request.ExpirationDays);
        var shortCode = await _mediator.Send(command);

        var shortUrl = $"{Request.Scheme}://{Request.Host}/{shortCode}";
        return Ok(new { ShortUrl = shortUrl });
    }

    [HttpGet("{shortCode}")]
    public async Task<IActionResult> GetOriginalUrl(string shortCode)
    {
        var query = new GetOriginalUrlQuery(shortCode);
        var originalUrl = await _mediator.Send(query);

        if (string.IsNullOrEmpty(originalUrl))
        {
            return NotFound();
        }

        return Redirect(originalUrl);
    }
}

public class CreateUrlRequest
{
    public string OriginalUrl { get; set; } = string.Empty;
    public int ExpirationDays { get; set; }
}
