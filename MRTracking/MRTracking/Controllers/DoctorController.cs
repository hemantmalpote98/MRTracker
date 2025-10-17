using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MRTracking.Filters;
using MRTracking.Models;
using MRTracking.Services;

namespace MRTracking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;
        private readonly IClaimService _claimService;

        public DoctorController(IDoctorService doctorService, IClaimService claimService)
        {
            _doctorService = doctorService;
            _claimService = claimService;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,MedicalRepresentative")]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetAllDoctors()
        {
            var doctors = await _doctorService.GetAllDoctorsAsync();
            return Ok(doctors);
        }

        [HttpGet("{medicalLicenseNumber}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,MedicalRepresentative")]
        public async Task<ActionResult<Doctor>> GetDoctorById(Guid medicalLicenseNumber)
        {
            var doctor = await _doctorService.GetDoctorByIdAsync(medicalLicenseNumber);
            if (doctor == null)
            {
                return NotFound();
            }
            return Ok(doctor);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,MedicalRepresentative")]
        public async Task<ActionResult> AddDoctor([FromBody] Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                await _doctorService.AddDoctorAsync(doctor);
                return CreatedAtAction(nameof(GetDoctorById), new { medicalLicenseNumber = doctor.MedicalLicenseNumber }, doctor);
            }

            return BadRequest(ModelState);  // Handle validation errors
        }


        [HttpPut("{medicalLicenseNumber}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,MedicalRepresentative")]
        public async Task<ActionResult> UpdateDoctor(string medicalLicenseNumber, [FromBody] Doctor doctor)
        {
            if (!medicalLicenseNumber.Equals(doctor.DoctorId.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                return BadRequest();
            }

            await _doctorService.UpdateDoctorAsync(doctor);
            return NoContent();
        }

        [HttpPut("{doctorId}/activate")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> ActivateDoctor(Guid doctorId)
        {
            var userId = Guid.NewGuid();
            await _doctorService.ActivateDoctorAsync(doctorId, userId);
            return Ok();
        }

        [HttpPut("{doctorId}/deactivate")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> DeactivateDoctor(Guid doctorId)
        {
            var userId = Guid.NewGuid();
            await _doctorService.DeactivateDoctorAsync(doctorId, userId);
            return Ok();
        }

        [HttpDelete("{doctorId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> DeleteDoctor(Guid doctorId)
        {
            var userId = Guid.NewGuid();
            await _doctorService.DeleteDoctorAsync(doctorId, userId);
            return Ok();
        }

        [HttpGet("available-doctors")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> GetAvailableDoctorsForGroup()
        {
            //var availableDoctors = await _doctorService.GetAllDoctorsAsync();
            var availableDoctors = await _doctorService.GetAvailableDoctorsForGroupAsync();
            return Ok(availableDoctors);
        }

        [HttpGet("available-doctors-edit/{groupId}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> GetAvailableDoctorsForEditGroup([FromRoute]Guid groupId)
        {
            //var availableDoctors = await _doctorService.GetAllDoctorsAsync();
            var availableDoctors = await _doctorService.GetAvailableDoctorsForEditGroupAsync(groupId);
            return Ok(availableDoctors);
        }

    }
}
