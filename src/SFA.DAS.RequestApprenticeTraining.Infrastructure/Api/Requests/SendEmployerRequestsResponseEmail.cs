using System.ComponentModel.Design;

namespace SFA.DAS.RequestApprenticeTraining.Infrastructure.Api.Requests
{
    public class SendEmployerRequestsResponseEmail
    {
        public Guid RequestedBy { get; set; }
        public long AccountId { get; set; }
        public List<StandardDetails> Standards { get; set; }
        public string ManageNotificationSettingsLink { get; set; }
        public string ManageRequestsLink { get; set; }
    }

    public class StandardDetails
    {
        public string StandardTitle { get; set; }
        public int StandardLevel { get; set; }

        public static explicit operator StandardDetails(Responses.StandardDetails source)
        {
            return new StandardDetails
            { 
                StandardTitle = source.StandardTitle,
                StandardLevel = source.StandardLevel,   
            };
        }
    }
}
