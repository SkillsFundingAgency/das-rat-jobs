using SFA.DAS.RequestApprenticeTraining.Domain.Types;
using System.Diagnostics.CodeAnalysis;

namespace SFA.DAS.RequestApprenticeTraining.Infrastructure.Api.Responses
{
    [ExcludeFromCodeCoverage]
    public class EmployerRequest
    {
        public Guid Id { get; set; }
        public RequestType RequestType { get; set; }
        public long AccountId { get; set; }
    }
}
