using EFCore.Identity.Learn.Context;
using EFCore.Identity.Learn.Dtos;
using EFCore.Identity.Learn.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EFCore.Identity.Learn.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserRolesController(ApplicationDbContext context,UserManager<AppUser> userManager) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateUserRole(CreateUserRoleDto request, CancellationToken cancellationToken)
        {
            #region 1.Yöntem 
            //AppUserRole appUserRole = new()
            //{
            //    UserId = request.UserId,
            //    RoleId = request.RoleId
            //};

            //await context.UserRoles.AddAsync(appUserRole);
            //await context.SaveChangesAsync(cancellationToken);

            #endregion

            #region 2. Yöntem 

            //Kullanıcıya rol atama ikinci yöntem   await userManager.AddToRoleAsync(appUser, "Admin");

            AppUser? appUser = await userManager.FindByIdAsync(request.UserId.ToString());
            if (appUser is null) return NotFound(new {Message = "Kullanıcı bulunamadı."});

            IdentityResult result = await userManager.AddToRoleAsync(appUser,"Admin");

            if (!result.Succeeded) return BadRequest(result.Errors.Select(s => s.Description));

            #endregion
            return NoContent();
            
        }

        [HttpPost]
        public async Task<IActionResult> RemoveByUserRole(string userId,string userRole)
        {
            AppUser? appUser = await userManager.FindByIdAsync(userId);
            if (appUser is null) return NotFound();

            IdentityResult result = await userManager.RemoveFromRoleAsync(appUser,userRole);
            if (!result.Succeeded) return BadRequest(result.Errors.Select(s=>s.Description));
            return NoContent();
        }
    }
}
