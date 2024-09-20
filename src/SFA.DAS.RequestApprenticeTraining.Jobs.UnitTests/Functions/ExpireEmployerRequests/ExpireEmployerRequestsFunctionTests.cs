using FluentAssertions;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api;
using SFA.DAS.RequestApprenticeTraining.Jobs.Functions.ExpireEmployerRequests;
using System;
using System.Threading.Tasks;

namespace SFA.DAS.RequestApprenticeTraining.Jobs.UnitTests.Functions.ExpireEmployerRequests
{
    [TestFixture]
    public class ExpireEmployerRequestsFunctionTests
    {
        private Mock<IEmployerRequestApprenticeTrainingOuterApi> _apiMock;
        private Mock<ILogger<ExpireEmployerRequestsFunction>> _loggerMock;
        private ExpireEmployerRequestsFunction _function;

        [SetUp]
        public void SetUp()
        {
            _apiMock = new Mock<IEmployerRequestApprenticeTrainingOuterApi>();
            _loggerMock = new Mock<ILogger<ExpireEmployerRequestsFunction>>();

            _function = new ExpireEmployerRequestsFunction(_apiMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task ExpireEmployerRequestsTimer_Should_Call_Api_ExpireEmployerRequests()
        {
            // Arrange
            var timerInfo = new TimerInfo();

            // Act
            await _function.ExpireEmployerRequestsTimer(timerInfo);

            // Assert
            _apiMock.Verify(api => api.ExpireEmployerRequests(), Times.Once);
        }

        [Test]
        public async Task ExpireEmployerRequestsTimer_Should_Log_Error_When_Exception_Occurs()
        {
            // Arrange
            var timerInfo = new TimerInfo();
            var exception = new Exception("Test exception");

            _apiMock
                .Setup(api => api.ExpireEmployerRequests())
                .Throws(exception);

            // Act
            Func<Task> act = async () => await _function.ExpireEmployerRequestsTimer(timerInfo);

            // Assert
            await act.Should().ThrowAsync<Exception>().WithMessage("Test exception");
        }
    }
}
