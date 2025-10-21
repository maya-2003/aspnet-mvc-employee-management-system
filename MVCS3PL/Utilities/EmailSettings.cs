using System.Net;
using System.Net.Mail;

namespace MVCS3PL.Utilities
{
    public static class EmailSettings
    {
        public static bool SendEmail(Email email)
        {
            try
            {
                var client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("maya.elb2003@gmail.com", "iann ugcp cvwu uilj");
                client.Send("maya.elb2003@gmail.com", email.To, email.Subject, email.Body);
                return true;

            }
            catch(Exception ex)
            {
                return false;

            }
            
            
        }
    }
}
