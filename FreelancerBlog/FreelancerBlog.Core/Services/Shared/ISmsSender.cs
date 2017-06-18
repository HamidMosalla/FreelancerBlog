using System.Threading.Tasks;

namespace FreelancerBlog.Core.Services.Shared
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
