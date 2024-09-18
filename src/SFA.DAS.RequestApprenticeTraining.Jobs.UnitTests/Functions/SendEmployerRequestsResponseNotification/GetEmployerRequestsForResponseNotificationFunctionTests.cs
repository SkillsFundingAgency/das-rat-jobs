using FluentAssertions;
using Microsoft.DurableTask;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api.Responses;
using SFA.DAS.Testing.AutoFixture;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFA.DAS.RequestApprenticeTraining.Jobs.Functions.SendEmployerRequestsResponseNotification.UnitTests
{
    public class GetEmployerRequestsForResponseNotificationFunctionTests
    {
        [Test, MoqAutoData]
        public async Task GetEmployerRequestsForResponseNotification_Should_Return_EmployerRequests(
            List<EmployerRequestResponseEmail> expectedRequests)
        {
            // Arrange
            var mockApi = new Mock<IEmployerRequestApprenticeTrainingOuterApi>();
            mockApi.Setup(s => s.GetEmployerRequestsForResponseNotification()).ReturnsAsync(expectedRequests);

            var mockLogger = new Mock<ILogger<GetEmployerRequestsForResponseNotificationActivity>>();
            var mockTaskActivityContext = new Mock<TaskActivityContext>();

            var sut = new GetEmployerRequestsForResponseNotificationActivity(mockApi.Object, mockLogger.Object);

            // Act
            var result = await sut.RunAsync(mockTaskActivityContext.Object, new object());

            // Assert
            mockApi.Verify(s => s.GetEmployerRequestsForResponseNotification(), Times.Once);
            result.Should().BeEquivalentTo(expectedRequests);
        }
    }
}
