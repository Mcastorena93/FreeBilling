namespace FreeBilling.Web.Services
{
    public class DevTimeEmailServices : IEmailServices
    {
        private readonly ILogger<DevTimeEmailServices> _logger;
        public DevTimeEmailServices(ILogger<DevTimeEmailServices> logger)
        {
            _logger = logger;
        }
        public void SendMail(string to, string from, string subject, string body)
        {
            _logger.LogInformation($"Sending email to {to} with a subject of {subject}.");
        }
    }
}
