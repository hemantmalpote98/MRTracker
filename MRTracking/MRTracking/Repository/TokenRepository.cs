using Microsoft.IdentityModel.Tokens;
using MRTracking.Constant;
using MRTracking.Models.IdentityModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MRTracking.Repository
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IMedicalRepresentativeRepository _medicalRepresentativeRepositor;
        public TokenRepository(IConfiguration configuration, IMedicalRepresentativeRepository medicalRepresentativeRepositor)
        {
            _configuration = configuration;
            _medicalRepresentativeRepositor = medicalRepresentativeRepositor;
        }
        public async Task<string> CreateJWTToken(ApplicationUser applicationUser, List<string>? roles)
        {
            //Create claim
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Email, applicationUser.Email));

            if (!roles.Any(a => a.Equals(Constants.Admin)) && roles.Any(a => a.Equals(Constants.MedicalRepresentative)))
            {
                var mr = await _medicalRepresentativeRepositor.GetRepresentativeByEmailIdAsync(applicationUser.Email);
                claims.Add(new Claim(Constants.UserId, mr.MedicalRepresentativeId.ToString()));
            }
            

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var keyResult = _configuration["Jwt:Key"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials (key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.Now.AddMinutes(30), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
