using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace THD.Core.Api.Helpers
{
    public static class EmailHelper
    {
        public async static Task<bool> SentGmail(string to_email, string subject, string content)
        {
            MailMessage mail = new MailMessage();
            bool resp = false;
            using (SmtpClient smtp = new SmtpClient())
            {
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = true;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.Credentials = new NetworkCredential("efilling.sys@gmail.com", "P@ssw0rd*1981");

                try
                {
                    mail.From = new MailAddress("efilling.sys@gmail.com");
                    mail.To.Add(to_email);
                    mail.Subject = subject;
                    mail.Body = content;
                    mail.IsBodyHtml = true;

                    smtp.Send(mail);
                    resp = true;

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                mail.Dispose();
                return resp;
            }

        }

    }
}
