using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api.Responses;
using SFA.DAS.RequestApprenticeTraining.Jobs.Functions.SendEmployerRequestsResponseNotification;
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
            var mockLogger = new Mock<ILogger>();

            mockApi.Setup(s => s.GetEmployerRequestsForResponseNotification()).ReturnsAsync(expectedRequests);

            var function = new GetEmployerRequestsForResponseNotificationFunction(mockApi.Object);

            // Act
            var result = await function.GetEmployerRequestsForResponseNotification(new object(), mockLogger.Object);

            // Assert
            mockApi.Verify(s => s.GetEmployerRequestsForResponseNotification(), Times.Once);
            result.Should().BeEquivalentTo(expectedRequests);
        }
    }
}
