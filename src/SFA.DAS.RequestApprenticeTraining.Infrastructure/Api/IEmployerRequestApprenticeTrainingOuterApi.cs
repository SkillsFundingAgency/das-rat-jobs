using RestEase;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api.Requests;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api.Responses;

namespace SFA.DAS.RequestApprenticeTraining.Infrastructure.Api
{
    public interface IEmployerRequestApprenticeTrainingOuterApi
    {

        [Post("/employerrequests/send-notifications")]
        Task SendProviderResponseNotifications([Body]SendProviderNotificationEmailRequest request);

        [Get("/employerrequests/requests-for-response-notification")]
        Task<List<EmployerRequestProviderResponseNotificationEmail>> GetEmployerRequestsForResponseNotification();

        [Post("/employerrequests/expire-requests")]
        Task ExpireEmployerRequests();

        [Get("/employerrequests/{employerRequestId}")]
        Task<EmployerRequest> GetEmployerRequest([Path] Guid employerRequestId);

        [Get("/ping")]
        Task Ping();
    }
}
