using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.RequestApprenticeTraining.Infrastructure.Configuration
{
    public class FunctionsOptions
    {
        public string ExpireEmployerRequestsTimerSchedule { get; set; }
        public string SendEmployerRequestsResponseNotificationTimerSchedule { get; set; }
        public string RefreshStandardsTimerSchedule { get; set; }
    }
}
