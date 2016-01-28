using System.Threading.Tasks;

namespace WebFor.Core.Services.Shared
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
