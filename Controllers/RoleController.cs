using EFCore.Identity.Learn.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Identity.Learn.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public sealed class RoleController(RoleManager<AppRole> roleManager) : ControllerBase
    {

        [HttpPost]

        public async Task<IActionResult> CreateRole(string roleName,CancellationToken cancellationToken)
        {
            AppRole appRole = new()
            {
                Name = roleName
            };

          IdentityResult result =   await roleManager.CreateAsync(appRole);
            if (!result.Succeeded) return BadRequest(result.Errors.Select(s => s.Description));

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRole(CancellationToken cancellationToken)
        {
            var roles = await roleManager.Roles.ToListAsync(cancellationToken);

            return Ok(roles);

        }
    }
}
