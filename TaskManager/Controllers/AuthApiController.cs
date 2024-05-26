using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManager.Abstractions.Services;
using TaskManager.Dtos;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthApiController(IAuthService authService): Controller
    {
        [HttpPost("signin")]
        public async Task<IActionResult> Signin(SigninDto authDto)
        {
            try 
            {
                await authService.AuthenticateAsync(authDto);
                return Ok("Успешный вход");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup(SignupDto authDto)
        {
            try
            {
                var registeredUserId = await authService.RegisterAsync(authDto);

                return Ok(registeredUserId);
            }
            catch(Exception ex)
            { 
                return BadRequest(ex.Message); 
            }
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            try
            {
                await authService.ConfirmEmailAsync(token, email);
                return Ok("Почта успешно подтверждена");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("become-admin")]
        public async Task<IActionResult> ChangeRoleOnAdmin()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            await authService.ChangeUserRoleOnAdmin(userEmail!);

            return Ok();
        }
    }
}
