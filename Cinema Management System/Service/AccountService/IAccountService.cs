using Cinema_Management_System.DataAccess;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Cinema_Management_System.Service.AccountService
{
    public interface IAccountService
    {
        bool IsLogined(ClaimsPrincipal User);
        Task SendMailAsync(ApplicationUser user, IUrlHelper url, HttpRequest request, EmailType emailType = EmailType.Register);

    }
}
