using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api.Requests;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api.Responses;
using SFA.DAS.Testing.AutoFixture;
using System.Threading.Tasks;

namespace SFA.DAS.RequestApprenticeTraining.Jobs.Functions.SendEmployerRequestsResponseNotification.UnitTests
{
    public class SendEmployerRequestsResponseNotificationsFunctionTests
    {
        [Test, MoqAutoData]
        public async Task SendProviderResponseNotifications_Should_Send_Notification(EmployerRequestResponseEmail email)
        {
            // Arrange
            var mockApi = new Mock<IEmployerRequestApprenticeTrainingOuterApi>();
            var mockLogger = new Mock<ILogger<SendEmployerRequestsResponseNotificationFunction>>();
            var function = new SendEmployerRequestsResponseNotificationFunction(mockLogger.Object, mockApi.Object);

            // Act
            await function.SendEmployerRequestsResponseNotification(email);

            // Assert
            mockApi.Verify(s => s.SendEmployerRequestsResponseNotification(It.IsAny<SendEmployerRequestsResponseEmail>()), Times.Once);
        }
    }
}
