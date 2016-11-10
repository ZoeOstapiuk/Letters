using System.Net;
using System.Net.Mail;

namespace Letters.Infrastructure
{
    public static class TempMailSender
    {
        private static string senderEmail = "zoeostapiuk@gmail.com";
        private static string senderPassword = "askaboutyachting30";
        private static string senderNickname = "zoeostapiuk";

        public static void SendTo(string to, string subject, string body)
        {
            var mail = new MailMessage();
            mail.To.Add(new MailAddress(to));
            mail.From = new MailAddress(senderEmail);

            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = senderNickname,
                    Password = senderPassword
                };

                smtp.Credentials = credential;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Send(mail);
            }
        }
    }
}