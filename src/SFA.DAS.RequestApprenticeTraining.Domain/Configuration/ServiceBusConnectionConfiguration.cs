using Microsoft.Extensions.Configuration;
using SFA.DAS.NServiceBus.Extensions;

namespace SFA.DAS.RequestApprenticeTraining.Domain.Configuration
{
    public class ServiceBusConnectionConfiguration
    {
        public static bool DoesNServiceBusUseManagedIdentity(IConfiguration configuration)
        {
            var connectionString = configuration.NServiceBusFullNamespace();
            if(string.IsNullOrEmpty(connectionString))
            {
                connectionString = configuration.NServiceBusSASConnectionString();
                if (connectionString == null)
                {
                    throw new InvalidOperationException("Cannot determine the authentication type for NServiceBus");
                }

                return false;
            }

            return true;
        }

        public static string GetNServiceBusConnectionString(IConfiguration configuration)
        {
            var connectionString = configuration.NServiceBusFullNamespace();
            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = configuration.NServiceBusSASConnectionString();
                if (connectionString == null)
                {
                    throw new InvalidOperationException("Cannot determine the connection string for NServiceBus");
                }
            }

            return connectionString;
        }
    }
}
