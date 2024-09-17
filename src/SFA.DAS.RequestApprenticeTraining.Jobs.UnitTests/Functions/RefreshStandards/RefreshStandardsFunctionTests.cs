using Microsoft.Azure.WebJobs.Extensions.Timers;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api;
using SFA.DAS.RequestApprenticeTraining.Jobs.Functions.RefreshStandards;
using System.Threading.Tasks;

namespace SFA.DAS.RequestApprenticeTraining.Functions.RefreshStandards.UnitTests
{
    public class RefreshStandardsTimerFunctionTests
    {
        private Mock<IEmployerRequestApprenticeTrainingOuterApi> _mockApi;
        private Mock<ILogger<RefreshStandardsFunction>> _mockLogger;
        private RefreshStandardsFunction _function;

        [SetUp]
        public void Setup()
        {
            // Arrange mocks
            _mockApi = new Mock<IEmployerRequestApprenticeTrainingOuterApi>();
            _mockLogger = new Mock<ILogger<RefreshStandardsFunction>>();

            // Initialize the function with the mocked dependencies
            _function = new RefreshStandardsFunction(_mockLogger.Object, _mockApi.Object);
        }

        [Test]
        public async Task RefreshStandardsTimer_Should_Call_RefreshStandards()
        {
            // Arrange
            var mockTimerInfo = new TimerInfo(null, new ScheduleStatus(), false);

            // Act
            await _function.RefreshStandardsTimer(mockTimerInfo);

            // Assert
            _mockApi.Verify(x => x.RefreshStandards(), Times.Once);
        }
    }
}
