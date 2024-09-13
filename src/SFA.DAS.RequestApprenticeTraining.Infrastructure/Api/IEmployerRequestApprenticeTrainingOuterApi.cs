using RestEase;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api.Requests;
using SFA.DAS.RequestApprenticeTraining.Infrastructure.Api.Responses;

namespace SFA.DAS.RequestApprenticeTraining.Infrastructure.Api
{
    public interface IEmployerRequestApprenticeTrainingOuterApi
    {

        [Post("/employer-requests/send-notification")]
        Task SendEmployerRequestsResponseNotification([Body]SendEmployerRequestsResponseEmail request);

        [Get("/employer-requests/response-notifications")]
        Task<List<EmployerRequestResponseEmail>> GetEmployerRequestsForResponseNotification();

        [Post("/employer-requests/expire")]
        Task ExpireEmployerRequests();

        [Get("/ping")]
        Task Ping();
    }
}
