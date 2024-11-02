using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace InventoryPharmacy.View
{
    public class EmailMessage
    {
        public EmailMessage() { }

        public void SendEmailMessage(string text)
        {
            string myEmail = "Goldenfreddi322@gmail.com";
            string emailBotLog = "bunkergametest@mail.ru";
            string emailBotPas = "netmVmX2s3YcmVuJ2hGD";
            string emailBotName = "Аптека";
            string htmlHeader = File.ReadAllText(@"C:\Users\donli\source\repos\InventoryPharmacy\InventoryPharmacy\Resource\htmlCodeForMessage.txt");

            SmtpClient Client = new SmtpClient();
            Client.Credentials = new NetworkCredential(emailBotLog, emailBotPas);
            Client.Host = "smtp.mail.ru";
            Client.Port = 587;
            Client.EnableSsl = true;

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(emailBotLog, emailBotName);
            mail.To.Add(new MailAddress(myEmail));
            mail.Subject = $"Товарный чек от {DateTime.Now}";
            mail.IsBodyHtml = true;
            mail.Body = $"{htmlHeader} \r\n{text} \r\n</table>\r\n\t\t\t\t\t</div><!--End Table-->\r\n";
            Client.Send(mail);
        }
    }
}
