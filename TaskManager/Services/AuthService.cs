using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using TaskManager.Abstractions.Services;
using TaskManager.Dtos;
using TaskManager.Identity;

namespace TaskManager.Services
{
    public class AuthService(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        IEmailService emailService) : IAuthService
    {
        public async Task AuthenticateAsync(SigninDto authDto)
        {
            var user = await userManager.FindByEmailAsync(authDto.Email);

            if (user == null)
            {
                throw new Exception("Пользователь с указанной почтой не существует");
            }

            var signinResult = await signInManager.PasswordSignInAsync(user, authDto.Password, false, false);
        }

        public async Task ConfirmEmailAsync(string confirmToken, string email)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                throw new Exception("Пользователь с таким email не найден");
            }

            var confirmResult = await userManager.ConfirmEmailAsync(user, confirmToken);

            if (!confirmResult.Succeeded)
            {
                throw new Exception(confirmResult.Errors.First().Description);
            }
        }

        public async Task<long> RegisterAsync(SignupDto authDto)
        {
            var user = new AppUser()
            {
                UserName = authDto.Username,
                Email = authDto.Email
            };

            var createResult = await userManager.CreateAsync(user, authDto.Password);

            if (createResult.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "user");

                var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                var encodedToken = HttpUtility.UrlEncode(token);
                var confirmationLink = $"https://localhost:7046/auth/confirm-email?token={encodedToken}&email={user.Email}";
                var message = $"Здравствуйте, {user.UserName}! Подтвердите регистрацию, перейдя по ссылке: <a href='{confirmationLink}'>Подтвердить почту</a>";
                await emailService.SendEmailAsync(user.Email, message);
                return user.Id;
            }
            else
            {
                throw new Exception(createResult.Errors.First().Description);
            }
        }

        public async Task ChangeUserRoleOnAdmin(string userEmail)
        {
            var user = await userManager.FindByEmailAsync(userEmail);

            if (user == null)
            {
                throw new Exception("Пользователь с таким email не найден");
            }

            await userManager.RemoveFromRoleAsync(user, "user");
            await userManager.AddToRoleAsync(user, "admin");
        }
    }
}
