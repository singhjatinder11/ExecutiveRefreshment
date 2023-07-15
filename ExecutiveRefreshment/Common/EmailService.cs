using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace ExecutiveRefreshment.Common
{
    public class EmailService
    {
        public static string SendEmail(string To_Mail, string Mail_Subject, string Body)
        {
            string SMTPServer = ConfigurationManager.AppSettings["SMTPServer"].ToString();
            string FromAddress = ConfigurationManager.AppSettings["FromAddress"].ToString();
            int Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);
            string Password = ConfigurationManager.AppSettings["Password"].ToString();

            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();

            SmtpClient SmtpServer = new System.Net.Mail.SmtpClient(SMTPServer);
            SmtpServer.Port = Port;//25;
            mail.From = new MailAddress(FromAddress);
            mail.To.Add(To_Mail);
            mail.Subject = Mail_Subject;
            mail.IsBodyHtml = true;
            mail.Body = Body;

            SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.EnableSsl = true;

            SmtpServer.Credentials = new NetworkCredential(FromAddress, Password);
            try
            {
                SmtpServer.Send(mail);
                return "success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}