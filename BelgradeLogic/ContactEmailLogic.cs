using Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BelgradeLogic
{
    public class ContactEmailLogic : IContactEmailLogic
    {
        public async Task<bool> SendEmail(ContactEmail email)
        {
            MailAddress from = new MailAddress(email.Email);
            MailAddress to = new MailAddress("acimov.teodora@gmail.com");
            MailMessage msg = new MailMessage(from, to);
            msg.Subject = "Where In Belgrade: " + email.Name + " " + email.Surname;
            msg.Body = email.Body;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.Credentials = new System.Net.NetworkCredential("medica.lenica@gmail.com", "MedicaLenica");
            smtp.EnableSsl = true;

            try
            {
                smtp.Send(msg);
            }
            catch (SmtpException)
            {
                return false;
            }
            return true;
        }
    }
}
