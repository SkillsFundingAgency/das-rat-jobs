using FluentAssertions;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api;
using SFA.DAS.RequestApprenticeTraining.Jobs.Functions.RefreshStandards;
using System;
using System.Threading.Tasks;

namespace SFA.DAS.RequestApprenticeTraining.Functions.RefreshStandards.UnitTests
{
    public class RefreshStandardsTimerFunctionTests
    {
        private Mock<IEmployerRequestApprenticeTrainingOuterApi> _apiMock;
        private Mock<ILogger<RefreshStandardsFunction>> _loggerMock;
        private RefreshStandardsFunction _function;

        [SetUp]
        public void Setup()
        {
            // Arrange mocks
            _apiMock = new Mock<IEmployerRequestApprenticeTrainingOuterApi>();
            _loggerMock = new Mock<ILogger<RefreshStandardsFunction>>();

            // Initialize the function with the mocked dependencies
            _function = new RefreshStandardsFunction(_loggerMock.Object, _apiMock.Object);
        }

        [Test]
        public async Task RefreshStandardsTimer_Should_Call_Api_RefreshStandards()
        {
            // Arrange
            var timerInfo = new TimerInfo();

            // Act
            await _function.RefreshStandardsTimer(timerInfo);

            // Assert
            _apiMock.Verify(api => api.RefreshStandards(), Times.Once);
        }

        [Test]
        public async Task RefreshStandardsTimer_Should_Log_Error_When_Exception_Occurs()
        {
            // Arrange
            var timerInfo = new TimerInfo();
            var exception = new Exception("Test exception");

            _apiMock
                .Setup(api => api.RefreshStandards())
                .Throws(exception);

            // Act
            Func<Task> act = async () => await _function.RefreshStandardsTimer(timerInfo);

            // Assert
            await act.Should().ThrowAsync<Exception>().WithMessage("Test exception");
        }
    }
}
