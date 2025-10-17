using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MRTracking.DTO;
using MRTracking.Models;
using MRTracking.Services;

namespace MRTracking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalRepresentativeVisitController : Controller
    {
        private readonly IMedicalRepresentativeVisitService _visitService;
        private readonly IScheduleVisitService _scheduleVisitService;

        public MedicalRepresentativeVisitController(IMedicalRepresentativeVisitService visitService, IScheduleVisitService scheduleVisitService)
        {
            _visitService = visitService;
            _scheduleVisitService = scheduleVisitService;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,MedicalRepresentative")]
        public async Task<ActionResult<IEnumerable<MedicalRepresentativeVisit>>> GetAllVisits()
        {
            var visits = await _visitService.GetAllVisitsAsync();
            return Ok(visits);
        }

        [HttpGet("{visitId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,MedicalRepresentative")]
        public async Task<ActionResult<MedicalRepresentativeVisit>> GetVisitById(int visitId)
        {
            var visit = await _visitService.GetVisitByIdAsync(visitId);
            if (visit == null)
            {
                return NotFound();
            }
            return Ok(visit);
        }

        [HttpPost]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,MedicalRepresentative")]
        public async Task<ActionResult> AddVisit([FromBody] MedicalRepresentativeVisit visit)
        {
            // Set default values for required fields if not provided
            if (visit.VisitUserType == 0) // Default enum value
            {
                visit.VisitUserType = VisitUserTypeEnum.Doctor; // Default to Doctor visit
            }

            await _visitService.AddVisitAsync(visit);
            return CreatedAtAction(nameof(GetVisitById), new { visitId = visit.VisitId }, visit);
        }

        [HttpPut("{visitId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,MedicalRepresentative")]
        public async Task<ActionResult> UpdateVisit(Guid visitId, [FromBody] MedicalRepresentativeVisit visit)
        {
            if (visitId != visit.VisitId)
            {
                return BadRequest();
            }

            await _visitService.UpdateVisitAsync(visit);
            return NoContent();
        }

        [HttpDelete("{visitId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,MedicalRepresentative")]
        public async Task<ActionResult> DeleteVisit(int visitId)
        {
            await _visitService.DeleteVisitAsync(visitId);
            return NoContent();
        }

        [HttpPost("/schedulevisit")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult> ScheduleVisit([FromBody]DateTime scheduleDate)
        {
            await _scheduleVisitService.CreateScheduleVisit(scheduleDate.Date);
            return NoContent();
        }
    }
}
