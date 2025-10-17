using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MRTracking.Models;
using MRTracking.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MRTracking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalRepresentativesController : ControllerBase
    {
        private readonly IMedicalRepresentativeService _service;
        private readonly IAuthService _authService;

        public MedicalRepresentativesController(IMedicalRepresentativeService service, IAuthService authService)
        {
            _service = service;
            _authService = authService;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<MedicalRepresentative>>> GetAllRepresentatives()
        {
            var representatives = await _service.GetAllRepresentativesAsync();
            return Ok(representatives);
        }

        [HttpGet("available-medicalrepresentative")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<MedicalRepresentative>>> GetAvailableRepresentativesForGroup()
        {
            var representatives = await _service.GetAvailableRepresentativesForGroup();
            return Ok(representatives);
        }

        [HttpGet("available-medicalrepresentative-edit/{groupId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<MedicalRepresentative>>> GetAvailableRepresentativesForEditGroup([FromRoute] Guid groupId)
        {
            var representatives = await _service.GetAvailableRepresentativesForEditGroup(groupId);
            return Ok(representatives);
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult<MedicalRepresentative>> GetRepresentativeById(string id)
        {
            var representative = await _service.GetRepresentativeByIdAsync(id);
            if (representative == null)
            {
                return NotFound();
            }
            return Ok(representative);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult> AddRepresentative([FromBody] MedicalRepresentative representative)
        {
            await _service.AddRepresentativeAsync(representative);
            await _authService.Register(new DTO.RegisterRequestDTO() { Password = "Pass@1234", Roles = new string[] { "MedicalRepresentative" }, UserName = representative.Email });
            return CreatedAtAction(nameof(GetRepresentativeById), new { id = representative.EmployeeID }, representative);
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult> UpdateRepresentative(string id, [FromBody] MedicalRepresentative representative)
        {
            representative.MedicalRepresentativeId.ToString();
            var mrId = representative.MedicalRepresentativeId.ToString();
            if (!id.Equals(mrId, StringComparison.InvariantCultureIgnoreCase))
            {
                return BadRequest();
            }
            await _service.UpdateRepresentativeAsync(representative);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult> DeleteRepresentative(string id)
        {
            await _service.DeleteRepresentativeAsync(id);
            return NoContent();
        }
    }
}
