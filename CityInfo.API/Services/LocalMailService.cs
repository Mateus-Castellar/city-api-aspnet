
namespace CityInfo.API.Services
{
    public class LocalMailService : ILocalMailService
    {
        private readonly string _mailTo = "admin@company.com";
        private readonly string _mailFrom = "noreply@company.com";

        public void Send(string subject, string message)
        {
            Console.WriteLine($"mail from {_mailFrom} to {_mailTo}, " +
                $"with {nameof(LocalMailService)}");

            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Message: {message}");
        }
    }
}