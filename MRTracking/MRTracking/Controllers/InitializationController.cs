using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MRTracking.Models;
using MRTracking.Models.IdentityModel;

namespace MRTracking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InitializationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ILogger<InitializationController> _logger;

        public InitializationController(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            ILogger<InitializationController> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        /// <summary>
        /// Initialize the application with default roles and admin user
        /// POST: api/Initialization/seed
        /// </summary>
        [HttpPost("seed")]
        public async Task<IActionResult> SeedData()
        {
            try
            {
                var result = new InitializationAPIModel()
                {
                    RolesCreated = new List<string>(),
                    RolesExisted = new List<string>(),
                    AdminUserCreated = false,
                    AdminUserExisted = false,
                    Messages = new List<string>()
                };

                // Define all roles
                var roles = new List<(string Id, string Name, string NormalizedName)>
                {
                    ("120bcbde-ff6e-49d5-913a-b488e2a8c860", "Admin", "ADMIN"),
                    ("91e08da7-5a91-4dd8-b809-1fa19007d587", "MedicalRepresentative", "MEDICALREPRESENTATIVE"),
                    ("2ab83b9d-b84f-4ec3-818e-e99e066ee000", "Doctor", "DOCTOR"),
                    ("8b7fc89b-645a-44da-81d8-e2a7faf6af6a", "MedicalStore", "MEDICALSTORE")
                };

                // Create roles if they don't exist
                foreach (var (id, name, normalizedName) in roles)
                {
                    var roleExists = await _roleManager.RoleExistsAsync(name);
                    if (!roleExists)
                    {
                        var role = new ApplicationRole
                        {
                            Id = Guid.Parse(id),
                            Name = name,
                            NormalizedName = normalizedName,
                            ConcurrencyStamp = id
                        };

                        var roleResult = await _roleManager.CreateAsync(role);
                        if (roleResult.Succeeded)
                        {
                            result.RolesCreated.Add(name);
                            _logger.LogInformation($"Role '{name}' created successfully.");
                        }
                        else
                        {
                            _logger.LogWarning($"Failed to create role '{name}': {string.Join(", ", roleResult.Errors.Select(e => e.Description))}");
                            result.Messages.Add($"Failed to create role '{name}': {string.Join(", ", roleResult.Errors.Select(e => e.Description))}");
                        }
                    }
                    else
                    {
                        result.RolesExisted.Add(name);
                        _logger.LogInformation($"Role '{name}' already exists.");
                    }
                }

                // Create admin user
                var adminEmail = "admin1@yopmail.com";
                var adminPassword = "Adm1n@123";

                var existingUser = await _userManager.FindByEmailAsync(adminEmail);
                if (existingUser == null)
                {
                    var adminUser = new ApplicationUser
                    {
                        UserName = adminEmail,
                        Email = adminEmail,
                        EmailConfirmed = true
                    };

                    var createUserResult = await _userManager.CreateAsync(adminUser, adminPassword);
                    if (createUserResult.Succeeded)
                    {
                        result.AdminUserCreated = true;
                        _logger.LogInformation($"Admin user '{adminEmail}' created successfully.");

                        // Assign Admin role to the user
                        var addToRoleResult = await _userManager.AddToRoleAsync(adminUser, "Admin");
                        if (addToRoleResult.Succeeded)
                        {
                            result.Messages.Add($"Admin user '{adminEmail}' created and assigned to Admin role.");
                            _logger.LogInformation($"Admin role assigned to user '{adminEmail}'.");
                        }
                        else
                        {
                            result.Messages.Add($"Admin user created but failed to assign Admin role: {string.Join(", ", addToRoleResult.Errors.Select(e => e.Description))}");
                            _logger.LogWarning($"Failed to assign Admin role to user '{adminEmail}': {string.Join(", ", addToRoleResult.Errors.Select(e => e.Description))}");
                        }
                    }
                    else
                    {
                        result.Messages.Add($"Failed to create admin user: {string.Join(", ", createUserResult.Errors.Select(e => e.Description))}");
                        _logger.LogWarning($"Failed to create admin user: {string.Join(", ", createUserResult.Errors.Select(e => e.Description))}");
                    }
                }
                else
                {
                    result.AdminUserExisted = true;
                    result.Messages.Add($"Admin user '{adminEmail}' already exists.");
                    _logger.LogInformation($"Admin user '{adminEmail}' already exists.");

                    // Check if user has Admin role
                    var isInRole = await _userManager.IsInRoleAsync(existingUser, "Admin");
                    if (!isInRole)
                    {
                        var addToRoleResult = await _userManager.AddToRoleAsync(existingUser, "Admin");
                        if (addToRoleResult.Succeeded)
                        {
                            result.Messages.Add($"Admin role assigned to existing user '{adminEmail}'.");
                            _logger.LogInformation($"Admin role assigned to existing user '{adminEmail}'.");
                        }
                    }
                    else
                    {
                        result.Messages.Add($"User '{adminEmail}' already has Admin role.");
                    }
                }

                return Ok(new
                {
                    Success = true,
                    Data = result,
                    Summary = new
                    {
                        TotalRolesCreated = result.RolesCreated.Count,
                        TotalRolesExisted = result.RolesExisted.Count,
                        AdminUserCreated = result.AdminUserCreated,
                        AdminUserExisted = result.AdminUserExisted
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding data.");
                return StatusCode(500, new
                {
                    Success = false,
                    Message = "An error occurred while initializing the application.",
                    Error = ex.Message
                });
            }
        }

        /// <summary>
        /// Get the status of roles and admin user
        /// GET: api/Initialization/status
        /// </summary>
        [HttpGet("status")]
        public async Task<IActionResult> GetStatus()
        {
            try
            {
                var roles = new List<string> { "Admin", "MedicalRepresentative", "Doctor", "MedicalStore" };
                var rolesStatus = new Dictionary<string, bool>();

                foreach (var role in roles)
                {
                    var exists = await _roleManager.RoleExistsAsync(role);
                    rolesStatus[role] = exists;
                }

                var adminEmail = "admin@yopmail.com";
                var adminUser = await _userManager.FindByEmailAsync(adminEmail);
                var adminExists = adminUser != null;
                var adminHasRole = false;

                if (adminExists)
                {
                    adminHasRole = await _userManager.IsInRoleAsync(adminUser, "Admin");
                }

                return Ok(new
                {
                    Roles = rolesStatus,
                    AdminUser = new
                    {
                        Email = adminEmail,
                        Exists = adminExists,
                        HasAdminRole = adminHasRole
                    },
                    IsInitialized = rolesStatus.Values.All(x => x) && adminExists && adminHasRole
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while checking status.");
                return StatusCode(500, new
                {
                    Success = false,
                    Message = "An error occurred while checking initialization status.",
                    Error = ex.Message
                });
            }
        }
    }
}

