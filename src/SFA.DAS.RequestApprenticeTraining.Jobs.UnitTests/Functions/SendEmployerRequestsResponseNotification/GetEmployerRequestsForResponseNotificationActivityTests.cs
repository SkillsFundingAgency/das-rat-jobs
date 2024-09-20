using FluentAssertions;
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
    public class GetEmployerRequestsForResponseNotificationActivityTests
    {
        [Test, MoqAutoData]
        public async Task GetEmployerRequestsForResponseNotification_Should_Return_EmployerRequests(
            List<EmployerRequestResponseEmail> expectedRequests)
        {
            // Arrange
            var mockApi = new Mock<IEmployerRequestApprenticeTrainingOuterApi>();
            mockApi.Setup(s => s.GetEmployerRequestsForResponseNotification()).ReturnsAsync(expectedRequests);

            var mockLogger = new Mock<ILogger<GetEmployerRequestsForResponseNotificationActivity>>();
            
            var sut = new GetEmployerRequestsForResponseNotificationActivity(mockApi.Object, mockLogger.Object);

            // Act
            var result = await sut.RunActivity(null);

            // Assert
            mockApi.Verify(s => s.GetEmployerRequestsForResponseNotification(), Times.Once);
            result.Should().BeEquivalentTo(expectedRequests);
        }
    }
}
