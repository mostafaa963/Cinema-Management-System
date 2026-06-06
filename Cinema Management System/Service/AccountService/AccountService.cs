using Cinema_Management_System.Areas.Identity.Controllers;
using Cinema_Management_System.DataAccess;
using Cinema_Management_System.Models;
using Cinema_Management_System.Repositories.IRepositories;
using Cinema_Management_System.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Cinema_Management_System.Service.AccountService
{
    public enum EmailType
    {
        Register,
        ResendConfirmation,
        ForgetPassword
    }

    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly IRepository<ApplicationUserOTP> _applicationUserOTPRepository;

        public AccountService(UserManager<ApplicationUser> userManager, IEmailSender emailSender, IRepository<ApplicationUserOTP> applicationUserOTPRepository)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _applicationUserOTPRepository = applicationUserOTPRepository;
        }

        public IEmailSender EmailSender => _emailSender;

        public bool IsLogined(ClaimsPrincipal User)
        {
            if (User is not null && User.Identity.IsAuthenticated)
                return true;

            return false;
        }

        public async Task SendMailAsync(ApplicationUser user, IUrlHelper url, HttpRequest request, EmailType emailType = EmailType.Register)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var link = url.Action(nameof(AccountController.Confirm), SD.ACCOUNT_CONTROLER, new { area = SD.IDENTITY_AREA, token, user.Id }, request.Scheme);

            string subject = string.Empty;
            string message = string.Empty;

            switch (emailType)
            {
                case EmailType.Register:
                    {
                        subject = "Confirmation Your Account in Cinema APP";
                        message = $"<h1>Confirm Your Account By Clicking <a href='{link}'>Here</a></h1>";
                    }
                    break;
                case EmailType.ResendConfirmation:
                    {
                        subject = "Resend - Confirmation Your Account in Cinema APP";
                        message = $"<h1>Confirm Your Account By Clicking <a href='{link}'>Here</a></h1>";
                    }
                    break;
                case EmailType.ForgetPassword:
                    {
                        var otp = new Random().Next(1000, 9999).ToString();

                        await _applicationUserOTPRepository.CreateAsync(new()
                        {
                            OTP = otp,
                            ApplicationUserId = user.Id,
                        });
                        await _applicationUserOTPRepository.CommitAsync();

                        subject = "Reset Password Your Account in Cinema APP";
                        message = $"Use this otp: {otp} to reset your password";
                    }
                    break;
            }

            await EmailSender.SendEmailAsync(user.Email!, subject, message);
        }
    }
}
