using FluentAssertions;
using Linky.Application.Urls.Commands;
using Linky.Domain.Entities;
using Linky.Domain.Interfaces;
using Moq;

namespace Linky.Tests;

public class CreateUrlShortCommandHandlerTests
{
    private readonly Mock<IUrlRepository> _urlRepositoryMock;
    private readonly CreateUrlShortCommandHandler _handler;

    public CreateUrlShortCommandHandlerTests()
    {
        _urlRepositoryMock = new Mock<IUrlRepository>();
        _handler = new CreateUrlShortCommandHandler(_urlRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldGenerateUniqueShortCode_AndSaveToDatabase()
    {
        // Arrange
        var command = new CreateUrlShortCommand("https://google.com", 7);
        _urlRepositoryMock.Setup(repo => repo.IsShortCodeUniqueAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
            
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNullOrWhiteSpace();
        result.Length.Should().Be(6); // As per our implementation
        _urlRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<UrlRecord>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
