using Microsoft.Azure.WebJobs.Extensions.Timers;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api;
using SFA.DAS.RequestApprenticeTraining.Jobs.Functions.ExpireEmployerRequests;
using System.Threading.Tasks;

namespace SFA.DAS.RequestApprenticeTraining.Functions.ExpireEmployerRequests.UnitTests
{
    public class ExpireEmployerRequestsTimerFunctionTests
    {
        private Mock<IEmployerRequestApprenticeTrainingOuterApi> _mockApi;
        private Mock<ILogger<ExpireEmployerRequestsFunction>> _mockLogger;
        private ExpireEmployerRequestsFunction _function;

        [SetUp]
        public void Setup()
        {
            // Arrange mocks
            _mockApi = new Mock<IEmployerRequestApprenticeTrainingOuterApi>();
            _mockLogger = new Mock<ILogger<ExpireEmployerRequestsFunction>>();

            // Initialize the function with the mocked dependencies
            _function = new ExpireEmployerRequestsFunction(_mockLogger.Object, _mockApi.Object);
        }

        [Test]
        public async Task ExpireEmployerRequestsTimer_Should_Call_ExpireEmployerRequests()
        {
            // Arrange
            var mockTimerInfo = new TimerInfo(null, new ScheduleStatus(), false);

            // Act
            await _function.ExpireEmployerRequestsTimer(mockTimerInfo);

            // Assert
            _mockApi.Verify(x => x.ExpireEmployerRequests(), Times.Once);
        }
    }
}
