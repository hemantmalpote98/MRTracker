using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MRTracking.DTO;
using MRTracking.Models.IdentityModel;
using MRTracking.Repository;
using MRTracking.Services;
using System.Web;

namespace MRTracking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenRepository _tokenRepository;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public AuthController(
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, 
            ITokenRepository tokenRepository,
            IEmailService emailService,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenRepository = tokenRepository;
            _emailService = emailService;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
        {
            var identityUser = new ApplicationUser { UserName = registerRequestDTO.UserName, Email = registerRequestDTO.UserName };
            var identityResult = await _userManager.CreateAsync(identityUser, registerRequestDTO.Password);
            if (identityResult.Succeeded)
            {
                if (registerRequestDTO.Roles is not null && registerRequestDTO.Roles.Any())
                {
                    identityResult = await _userManager.AddToRolesAsync(identityUser, registerRequestDTO.Roles);
                    if (identityResult.Succeeded) { 
                     return Ok("User registered successfully");
                    }
                }
                
            }
            return BadRequest("Something wend wrong");
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginRequestDTO.UserName);
            if (user is not null)
            {
                var hasValidPassword = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);
                if (hasValidPassword)
                {
                    // Check roles
                    var roles = await _userManager.GetRolesAsync(user);
                    if (roles is not null && roles.Any()) {
                        return Ok(await _tokenRepository.CreateJWTToken(user, roles.ToList()));
                    }
                }

            }
            return BadRequest("Incorrect credentials");
        }

        [HttpPost]
        [Route("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequestDTO forgotPasswordRequest)
        {
            var user = await _userManager.FindByEmailAsync(forgotPasswordRequest.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist for security reasons
                return Ok("If your email is registered, you will receive a password reset link.");
            }

            // Generate password reset token
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            
            // URL encode the token to handle special characters
            var encodedToken = HttpUtility.UrlEncode(token);

            // Create reset link (you'll need to adjust this URL based on your frontend)
            var resetLink = $"{_configuration["AppSettings:FrontendUrl"]}/reset-password?email={forgotPasswordRequest.Email}&token={encodedToken}";

            // Email body
            var emailBody = $@"
                <html>
                <body>
                    <h2>Password Reset Request</h2>
                    <p>Hello,</p>
                    <p>You requested to reset your password. Please click the link below to reset your password:</p>
                    <p><a href='{resetLink}'>Reset Password</a></p>
                    <p>If you did not request this, please ignore this email.</p>
                    <p>This link will expire in 24 hours.</p>
                    <br/>
                    <p>Best regards,<br/>MR Tracking System</p>
                </body>
                </html>
            ";

            try
            {
                await _emailService.SendEmailAsync(forgotPasswordRequest.Email, "Password Reset Request", emailBody);
                return Ok("If your email is registered, you will receive a password reset link.");
            }
            catch (Exception ex)
            {
                // Log the error but don't expose it to the user
                return StatusCode(500, "An error occurred while processing your request. Please try again later.");
            }
        }

        [HttpPost]
        [Route("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDTO resetPasswordRequest)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordRequest.Email);
            if (user == null)
            {
                return BadRequest("Invalid request.");
            }

            // Decode the token
            var decodedToken = HttpUtility.UrlDecode(resetPasswordRequest.Token);

            // Reset the password
            var result = await _userManager.ResetPasswordAsync(user, decodedToken, resetPasswordRequest.NewPassword);
            
            if (result.Succeeded)
            {
                return Ok("Password has been reset successfully.");
            }

            return BadRequest(new { Errors = result.Errors.Select(e => e.Description) });
        }
    }
}
