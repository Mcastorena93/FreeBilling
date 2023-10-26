namespace FreeBilling.Web.Services
{
    public interface IEmailServices
    {
        void SendMail(string to, string from, string subject, string body);

    }
}
