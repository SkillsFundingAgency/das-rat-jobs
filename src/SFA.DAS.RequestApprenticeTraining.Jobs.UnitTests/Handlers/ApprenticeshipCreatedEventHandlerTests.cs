using AutoFixture;
using NServiceBus.Testing;
using NUnit.Framework;
using SFA.DAS.CommitmentsV2.Messages.Events;
using SFA.DAS.RequestApprenticeTraining.Jobs.Handlers.ApprovalsHandlers;
using SFA.DAS.Testing.AutoFixture;
using System.Threading.Tasks;

namespace SFA.DAS.RequestApprenticeTraining.Jobs.UnitTests.Handlers
{
    public class ApprenticeshipCreatedEventHandlerTests
    {
        private readonly Fixture _fixture = new Fixture();

        [Test, MoqAutoData]
        public async Task Handle_ShouldCallApi_WhenMessageIsReceived(
            ApprenticeshipCreatedEventHandler sut)
        {
            // Arrange
            var message = _fixture.Build<ApprenticeshipCreatedEvent>()
               .Create();

            var messageHandlerContext = new TestableMessageHandlerContext();

            // Act
            await sut.Handle(message, messageHandlerContext);

            // Assert
        }
    }
}