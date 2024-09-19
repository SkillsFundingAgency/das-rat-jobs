using Microsoft.DurableTask;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api.Responses;
using SFA.DAS.Testing.AutoFixture;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFA.DAS.RequestApprenticeTraining.Jobs.Functions.SendEmployerRequestsResponseNotification.UnitTests
{
    public class SendEmployerRequestsResponseNotificationOrchestrationTests
    {
        [Test, MoqAutoData]
        public async Task Orchestrator_Should_Call_GetEmployerRequestsAndSendNotifications(List<EmployerRequestResponseEmail> employerRequests)
        {
            // Arrange
            var mockContext = new Mock<TaskOrchestrationContext>();

            mockContext
                .Setup(x => x.CallActivityAsync<List<EmployerRequestResponseEmail>>(nameof(GetEmployerRequestsForResponseNotificationActivity), null, null))
                .ReturnsAsync(employerRequests);

            mockContext
                .Setup(x => x.CreateReplaySafeLogger(It.Is<Type>(p => p == typeof(SendEmployerRequestsResponseNotificationOrchestration))))
                .Returns(new Mock<ILogger>().Object);

            // Act
            await SendEmployerRequestsResponseNotificationOrchestration.RunOrchestrator(mockContext.Object);

            // Assert
            mockContext
                .Verify(x => x.CallActivityAsync<List<EmployerRequestResponseEmail>>(nameof(GetEmployerRequestsForResponseNotificationActivity), null, null), Times.Once);

            foreach (var request in employerRequests)
            {
                mockContext
                    .Verify(x => x.CallActivityAsync(nameof(SendEmployerRequestsResponseNotificationActivity), request, null), Times.Once);
            }
        }
    }
}
