using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MRTracking.Models;
using MRTracking.Services;
using System.Text.RegularExpressions;

namespace MRTracking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MRGroupController : Controller
    {
        private readonly IMRGroupService _groupService;

        public MRGroupController(IMRGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> GetAllGroups()
        {
            var groups = await _groupService.GetAllGroupsAsync();
            return Ok(groups);
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> GetGroupById(Guid id)
        {
            var group = await _groupService.GetGroupByIdAsync(id);
            if (group == null)
            {
                return NotFound();
            }
            return Ok(group);
        }

        //[HttpPost]
        //public async Task<IActionResult> AddGroup([FromBody] MRGroupRequest groupRequest)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Map MRGroupRequest to MRGroup
        //        var group = new MRGroup
        //        {
        //            MRGroupId = Guid.NewGuid(), // Generate a new ID for the group
        //            GroupName = groupRequest.GroupName,
        //            Location = groupRequest.Location,
        //            IsActive = groupRequest.IsActive,
        //            IsDeleted = groupRequest.IsDeleted,
        //            CreatedOn = DateTime.UtcNow,
        //            CreatedBy = Guid.NewGuid(), // Replace with actual logged-in user ID
        //            Doctors = groupRequest.Doctors?.Select(d => new Doctor { DoctorId = d, Phone = "123456789" }).ToList(),
        //            MedicalStores = groupRequest.MedicalStores?.Select(ms => new MedicalStore { MedicalStoreId = ms }).ToList(),
        //            MedicalRepresentatives = groupRequest.MedicalRepresentatives?.Select(mr => new MedicalRepresentative { MedicalRepresentativeId = mr }).ToList()
        //        };

        //        await _groupService.AddGroupAsync(group);
        //        return Ok(group);
        //    }
        //    return BadRequest(ModelState);
        //}

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> AddGroup([FromBody] MRGroupRequest groupRequest)
        {
            if (ModelState.IsValid)
            {
                // Map MRGroupRequest to MRGroup
                var group = new MRGroup
                {
                    MRGroupId = Guid.NewGuid(), // Generate a new ID for the group
                    GroupName = groupRequest.GroupName,
                    Location = groupRequest.Location,
                    IsActive = groupRequest.IsActive,
                    IsDeleted = groupRequest.IsDeleted,
                    CreatedOn = DateTime.UtcNow,
                    CreatedBy = Guid.NewGuid() // Replace with actual logged-in user ID
                };

                await _groupService.AddGroupAsync(group, groupRequest.Doctors.ToList(), groupRequest.MedicalStores.ToList(), groupRequest.MedicalRepresentatives.ToList());
                return Ok(group);
            }
            return BadRequest(ModelState);
        }


        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> UpdateGroup(Guid id, [FromBody] MRGroupRequest groupRequest)
        {
            var existingGroup = await _groupService.GetGroupByIdAsync(id);
            if (existingGroup == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                existingGroup.GroupName = groupRequest.GroupName;
                existingGroup.Location = groupRequest.Location;
                existingGroup.Doctors = groupRequest?.Doctors?.Select(d => new Doctor() { DoctorId = d }).ToList();
                existingGroup.MedicalStores = groupRequest?.MedicalStores?.Select(m => new MedicalStore() { MedicalStoreId = m }).ToList();
                existingGroup.MedicalRepresentatives = groupRequest?.MedicalRepresentatives?.Select(m => new MedicalRepresentative() { MedicalRepresentativeId = m }).ToList();
                // Map MRGroupRequest to MRGroup
                await _groupService.UpdateGroupAsync(existingGroup);
                return Ok(existingGroup);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> DeleteGroup(Guid id)
        {
            var existingGroup = await _groupService.GetGroupByIdAsync(id);
            if (existingGroup == null)
            {
                return NotFound();
            }

            await _groupService.DeleteGroupAsync(id);
            return NoContent();
        }

        // Adding removing doctor and medical store to group

        [HttpPost("{groupId}/medicalstores/{medicalStoreId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> AddMedicalStoreToMRGroup(Guid groupId, Guid medicalStoreId)
        {
            await _groupService.AddMedicalStoreToGroupAsync(groupId, medicalStoreId);
            return Ok();
        }

        [HttpPost("{groupId}/doctors/{doctorId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> AddDoctorToMRGroup(Guid groupId, Guid doctorId)
        {
            await _groupService.AddDoctorToGroupAsync(groupId, doctorId);
            return Ok();
        }

        [HttpDelete("{groupId}/medicalstores/{medicalStoreId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> RemoveMedicalStoreFromGroup(Guid groupId, Guid medicalStoreId)
        {
            await _groupService.RemoveMedicalStoreFromGroupAsync(groupId, medicalStoreId);
            return NoContent();
        }

        [HttpDelete("{groupId}/doctors/{doctorId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> RemoveDoctorFromMRGroup(Guid groupId, Guid doctorId)
        {
            await _groupService.RemoveDoctorFromGroupAsync(groupId, doctorId);
            return NoContent();
        }

    }
}
