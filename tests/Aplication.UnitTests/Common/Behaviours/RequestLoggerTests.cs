using System.Threading;
using System.Threading.Tasks;
using BibliothecaManager.Application.Authors.Commands.Create;
using BibliothecaManager.Application.Common.Behaviours;
using BibliothecaManager.Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace BibliothecaManager.Application.UnitTests.Common.Behaviours;

public class RequestLoggerTests
{
    private readonly Mock<ILogger<CreateAuthorCommand>> _logger;
    private readonly Mock<ICurrentUserService> _currentUserService;
    private readonly Mock<IIdentityService> _identityService;


    public RequestLoggerTests()
    {
        _logger = new Mock<ILogger<CreateAuthorCommand>>();

        _currentUserService = new Mock<ICurrentUserService>();

        _identityService = new Mock<IIdentityService>();
    }

    [Test]
    public async Task ShouldCallGetUserNameAsyncOnceIfAuthenticated()
    {
        _currentUserService.Setup(x => x.UserId).Returns("Administrator");

        var requestLogger = new LoggingBehaviour<CreateAuthorCommand>(_logger.Object, _currentUserService.Object, _identityService.Object);

        await requestLogger.Process(new CreateAuthorCommand("Test", "Surname-test"), new CancellationToken());

        _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Once);
    }

    [Test]
    public async Task ShouldNotCallGetUserNameAsyncOnceIfUnauthenticated()
    {
        var requestLogger = new LoggingBehaviour<CreateAuthorCommand>(_logger.Object, _currentUserService.Object, _identityService.Object);

        await requestLogger.Process(new CreateAuthorCommand("Test", "Surname-test"), new CancellationToken());

        _identityService.Verify(i => i.GetUserNameAsync(null), Times.Never);
    }
}
