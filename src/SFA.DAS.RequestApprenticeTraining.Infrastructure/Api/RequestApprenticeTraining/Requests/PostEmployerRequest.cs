using SFA.DAS.RequestApprenticeTraining.Domain.Types;

namespace SFA.DAS.RequestApprenticeTraining.Infrastructure.Api.RequestApprenticeTraining.Requests
{
    public class PostEmployerRequest
    {
        public string EncodedAccountId { get; set; }
        public RequestType RequestType { get; set; }
    }
}
