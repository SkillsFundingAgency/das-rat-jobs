using RestEase;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api.Responses;

namespace SFA.DAS.RequestApprenticeTraining.Infrastructure.Api
{
    public interface IEmployerRequestApprenticeTrainingOuterApi
    {
        [Get("/employerrequests/{employerRequestId}")]
        Task<EmployerRequest> GetEmployerRequest([Path] Guid employerRequestId);

        [Get("/ping")]
        Task Ping();
    }
}
