using SFA.DAS.RequestApprenticeTraining.Domain.Types;
using System.Diagnostics.CodeAnalysis;

namespace SFA.DAS.RequestApprenticeTraining.Infrastructure.Api.Responses
{
    [ExcludeFromCodeCoverage]
    public class EmployerRequestResponseEmail
    {
        public Guid RequestedBy { get; set; }
        public long AccountId { get; set; }
        public List<StandardDetails> Standards { get; set; }
    }

    public class StandardDetails
    {
        public string StandardTitle { get; set; }
        public int StandardLevel { get; set; }
    }
}
