using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TwentiBeauti_BackEnd_DotNet.Controllers
{
    [Route("api/")]
    [ApiController]
    public class EmailController : Controller
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        public void SendEmail(string toAddress, string subject, string content)
        {
            MailAddress to = new MailAddress(toAddress);
            MailAddress from = new MailAddress("20520556@gm.uit.edu.vn");

            MailMessage message = new MailMessage(from, to);
            message.Subject = subject;
            message.Body = content;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("20520556@gm.uit.edu.vn", "Giahuytrinh.26102002"),
                EnableSsl = true
                // specify whether your host accepts SSL connections
            };
            // code in brackets above needed if authentication required
            try
            {
                client.Send(message);
            }
            catch (SmtpException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }



    }
}
