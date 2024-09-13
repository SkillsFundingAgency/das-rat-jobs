using RestEase;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api.Requests;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api.Responses;

namespace SFA.DAS.RequestApprenticeTraining.Infrastructure.Api
{
    public interface IEmployerRequestApprenticeTrainingOuterApi
    {

        [Post("/employerrequests/send-notifications")]
        Task SendEmployerRequestsResponseNotification([Body]SendEmployerRequestsResponseEmail request);

        [Get("/employerrequests/requests-for-response-notification")]
        Task<List<EmployerRequestResponseEmail>> GetEmployerRequestsForResponseNotification();

        [Post("/employerrequests/expire-requests")]
        Task ExpireEmployerRequests();

        [Get("/employerrequests/{employerRequestId}")]
        Task<EmployerRequest> GetEmployerRequest([Path] Guid employerRequestId);

        [Get("/ping")]
        Task Ping();
    }
}
