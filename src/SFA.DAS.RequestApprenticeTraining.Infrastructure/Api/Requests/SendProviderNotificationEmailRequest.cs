using System.ComponentModel.Design;

namespace SFA.DAS.RequestApprenticeTraining.Infrastructure.Api.Requests
{
    public class SendProviderNotificationEmailRequest
    {
        public Guid RequestedBy { get; set; }
        public long AccountId { get; set; }
        public List<StandardDetails> Standards { get; set; }
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
