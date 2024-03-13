using SFA.DAS.Http.Configuration;

namespace SFA.DAS.RequestApprenticeTraining.Infrastructure.Api.RequestApprenticeTraining
{
    public class RequestApprenticeTrainingApiClientConfiguration : IManagedIdentityClientConfiguration
    {
        public string IdentifierUri { get; set; }
        public string ApiBaseUrl { get; set; }
    }
}
