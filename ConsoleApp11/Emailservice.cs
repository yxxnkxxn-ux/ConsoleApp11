using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Net;
using System.Net.Mail;

namespace AssignmentNotifier
{
    public class EmailService
    {
        private string _email;
        private string _password;

        public EmailService(string email, string password)
        {
            _email = email;
            _password = password;
        }

        public void SendMail(string toEmail, string subject, string body)
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(_email, _password);

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(_email);
                mail.To.Add(toEmail);
                mail.Subject = subject;
                mail.Body = body;

                client.Send(mail);

                Console.WriteLine("이메일 전송 완료!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("이메일 전송 실패: " + ex.Message);
            }
        }
    }
}
