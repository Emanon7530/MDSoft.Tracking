namespace MDSoft.Tracking.API.Utilities
{
    public interface IMailService
    {
        bool SendMail(MailData mailData);
    }
}
