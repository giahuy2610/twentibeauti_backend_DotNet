using TwentiBeauti_BackEnd_DotNet.Models;
using System.Threading.Tasks;
namespace TwentiBeauti_BackEnd_DotNet.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
