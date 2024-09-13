namespace SFA.DAS.RequestApprenticeTraining.Infrastructure.Configuration
{
    public class ApplicationConfiguration
    {
        public EmployerRequestApprenticeTrainingOuterApiConfiguration EmployerRequestApprenticeTrainingOuterApiConfiguration { get; set; }
        public FunctionsOptions FunctionOptions { get; set; }
        public string EmployerRequestApprenticeshipTrainingBaseUrl { get; set; }
        public string EmployerAccountsBaseUrl { get; set; }
    }
}
