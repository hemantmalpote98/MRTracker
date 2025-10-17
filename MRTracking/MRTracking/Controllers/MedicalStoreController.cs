using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MRTracking.Models;
using MRTracking.Services;

namespace MRTracking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalStoreController : Controller
    {
        private readonly IMedicalStoreService _medicalStoreService;

        public MedicalStoreController(IMedicalStoreService medicalStoreService)
        {
            _medicalStoreService = medicalStoreService;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,MedicalRepresentative")]
        public async Task<IActionResult> GetAllMedicalStores()
        {
            var medicalStores = await _medicalStoreService.GetAllMedicalStoresAsync();
            return Ok(medicalStores);
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,MedicalRepresentative")]
        public async Task<IActionResult> GetMedicalStoreById(Guid id)
        {
            var medicalStore = await _medicalStoreService.GetMedicalStoreByIdAsync(id);
            if (medicalStore == null)
            {
                return NotFound();
            }
            return Ok(medicalStore);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,MedicalRepresentative")]
        public async Task<IActionResult> AddMedicalStore([FromBody] MedicalStore medicalStore)
        {
            if (ModelState.IsValid)
            {
                await _medicalStoreService.AddMedicalStoreAsync(medicalStore);
                return Ok(medicalStore);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,MedicalRepresentative")]
        public async Task<IActionResult> UpdateMedicalStore(Guid id, [FromBody] MedicalStore medicalStore)
        {
            if (id != medicalStore.MedicalStoreId)
            {
                return BadRequest("Medical Store ID mismatch");
            }

            //var existingMedicalStore = await _medicalStoreService.GetMedicalStoreByIdAsync(id);
            //if (existingMedicalStore == null)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                await _medicalStoreService.UpdateMedicalStoreAsync(medicalStore);
                return Ok(medicalStore);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> DeleteMedicalStore(Guid id)
        {
            var existingMedicalStore = await _medicalStoreService.GetMedicalStoreByIdAsync(id);
            if (existingMedicalStore == null)
            {
                return NotFound();
            }

            await _medicalStoreService.DeleteMedicalStoreAsync(id);
            return NoContent();
        }

        [HttpPut("{medicalStoreId}/activate")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> ActivateDoctor(Guid medicalStoreId)
        {
            var userId = Guid.NewGuid();
            await _medicalStoreService.ActivateMedicalStoreAsync(medicalStoreId, userId);
            return Ok();
        }

        [HttpPut("{medicalStoreId}/deactivate")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> DeactivateDoctor(Guid medicalStoreId)
        {
            var userId = Guid.NewGuid();
            await _medicalStoreService.DeactivateMedicalStoreAsync(medicalStoreId, userId);
            return Ok();
        }

        [HttpGet("available-medical-stores")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> GetAvailableMedicalStoresForGroup()
        {
            //var medicalStores = new List<MedicalStore>();
            var medicalStores = await _medicalStoreService.GetAvailableMedicalStoresForGroupAsync();
            return Ok(medicalStores);
        }

        [HttpGet("available-medical-stores-edit/{groupId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> GetAvailableMedicalStoresForEditGroup(Guid groupId)
        {
            //var medicalStores = new List<MedicalStore>();
            var medicalStores = await _medicalStoreService.GetAvailableMedicalStoresForEditGroupAsync(groupId);
            return Ok(medicalStores);
        }
    }
}
