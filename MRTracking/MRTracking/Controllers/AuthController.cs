using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MRTracking.DTO;
using MRTracking.Models.IdentityModel;
using MRTracking.Repository;

namespace MRTracking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenRepository _tokenRepository;

        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ITokenRepository tokenRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenRepository = tokenRepository;
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
    }
}
