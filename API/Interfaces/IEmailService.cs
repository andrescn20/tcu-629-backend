using DataAccess.Models;
using DTO;

namespace API.Interfaces
{
    public interface IEmailService
    {
        Task SendResetPasswordEmail(string email, string resetLink);


    }
}
