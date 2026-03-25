using FluentAssertions;
using Linky.Application.Urls.Queries;
using Linky.Domain.Entities;
using Linky.Domain.Interfaces;
using Moq;

namespace Linky.Tests;

public class GetOriginalUrlQueryHandlerTests
{
    private readonly Mock<IUrlRepository> _urlRepositoryMock;
    private readonly GetOriginalUrlQueryHandler _handler;

    public GetOriginalUrlQueryHandlerTests()
    {
        _urlRepositoryMock = new Mock<IUrlRepository>();
        _handler = new GetOriginalUrlQueryHandler(_urlRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnOriginalUrl_WhenExistsAndNotExpired()
    {
        // Arrange
        var shortCode = "ABCDEF";
        var originalUrl = "https://google.com";
        var record = new UrlRecord
        {
            OriginalUrl = originalUrl,
            ShortCode = shortCode,
            ExpiresAt = DateTime.UtcNow.AddDays(1),
            AccessCount = 0
        };

        _urlRepositoryMock.Setup(repo => repo.GetByShortCodeAsync(shortCode, It.IsAny<CancellationToken>()))
            .ReturnsAsync(record);

        var query = new GetOriginalUrlQuery(shortCode);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().Be(originalUrl);
        record.AccessCount.Should().Be(1); // Since it was accessed
        _urlRepositoryMock.Verify(repo => repo.UpdateAsync(record, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnNull_WhenExpired()
    {
        // Arrange
        var shortCode = "ABCDEF";
        var record = new UrlRecord
        {
            OriginalUrl = "https://google.com",
            ShortCode = shortCode,
            ExpiresAt = DateTime.UtcNow.AddDays(-1) // Expired
        };

        _urlRepositoryMock.Setup(repo => repo.GetByShortCodeAsync(shortCode, It.IsAny<CancellationToken>()))
            .ReturnsAsync(record);

        var query = new GetOriginalUrlQuery(shortCode);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeNull();
        record.AccessCount.Should().Be(0); // Should not increment
        _urlRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<UrlRecord>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}
