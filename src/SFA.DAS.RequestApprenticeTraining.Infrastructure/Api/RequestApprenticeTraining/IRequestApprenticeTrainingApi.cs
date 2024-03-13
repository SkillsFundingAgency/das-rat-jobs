using RestEase;
using SFA.DAS.RequestApprenticeTraining.Domain.Types;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api.RequestApprenticeTraining.Requests;

namespace SFA.DAS.RequestApprenticeTraining.Infrastructure.Api.RequestApprenticeTraining
{
    public interface IRequestApprenticeTrainingApi
    {
        [Get("/employerrequest/{employerRequestId}")]
        Task<EmployerRequest> GetEmployerRequest([Path] Guid employerRequestId);

        [Get("/employerrequest/account/{accountId}")]
        Task<List<EmployerRequest>> GetEmployerRequests([Path] long accountId);

        [Post("/employerrequest")]
        Task<Guid> CreateEmployerRequest([Body] PostEmployerRequest request);

        [Get("/ping")]
        Task Ping();
    }
}
