using System.Threading.Tasks;

namespace WebFor.Core.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
