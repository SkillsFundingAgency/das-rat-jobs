using Microsoft.DurableTask;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api.Requests;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api.Responses;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Configuration;
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
            var mockLogger = new Mock<ILogger<SendEmployerRequestsResponseNotificationActivity>>();

            var mockTaskActivityContext = new Mock<TaskActivityContext>();

            var config = new ApplicationConfiguration()
            {
                EmployerAccountsBaseUrl = "http://employeraccounts/",
                EmployerRequestApprenticeshipTrainingBaseUrl = $"http://employerratweb/",
            };
            var mockOptions = new Mock<IOptions<ApplicationConfiguration>>();
            mockOptions.Setup(o => o.Value).Returns(config);

            var sut = new SendEmployerRequestsResponseNotificationActivity(mockApi.Object, mockLogger.Object, mockOptions.Object);

            // Act
            await sut.RunAsync(mockTaskActivityContext.Object, email);

            // Assert
            var expectedManageRequestsLink = $"{config.EmployerRequestApprenticeshipTrainingBaseUrl}{{0}}/dashboard";
            var expectedNotificationsLink = $"{config.EmployerAccountsBaseUrl}settings/notifications";

            mockApi.Verify(s => s.SendEmployerRequestsResponseNotification(It.Is<SendEmployerRequestsResponseEmail>(e =>
                e.AccountId == email.AccountId &&
                e.RequestedBy == email.RequestedBy &&
                e.ManageNotificationSettingsLink == expectedNotificationsLink &&
                e.ManageRequestsLink == expectedManageRequestsLink
            )), Times.Once);
        }
    }
}
