using EFCore.Identity.Learn.Dtos;
using EFCore.Identity.Learn.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace EFCore.Identity.Learn.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> RegisterDto(RegisterDto request,CancellationToken cancellationToken)
        {
            AppUser appUser = new()
            {
                Email = request.Email,
                UserName = request.UserName,
                FirstName = request.FirstName,
                LastName = request.LastName,

            };

           IdentityResult result =  await userManager.CreateAsync(appUser,request.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);


            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto request,CancellationToken cancellationToken) 
        {

            AppUser? appUser = await userManager.FindByIdAsync(request.id.ToString());
            if (appUser == null)
                return BadRequest(new { Message = "Kullanıcı bulanamadı" });


           IdentityResult result =  await userManager.ChangePasswordAsync(appUser,request.CurrentPassword,request.NewPassword);
            if(!result.Succeeded)
                return BadRequest(result.Errors.Select(s=>s.Description));

            return NoContent();
        }



        [HttpGet]
        public async Task<IActionResult> ForgotPassword(string email, CancellationToken cancellationToken)
        {
            AppUser? appUser = await userManager.FindByEmailAsync(email);
            if (appUser == null)
                return BadRequest(new { Message = "Kullanıcı bulanamadı" });

            string token = await userManager.GeneratePasswordResetTokenAsync(appUser);

            return Ok(new {Token =  token});
        }

        [HttpPost]
        public async Task<IActionResult> ChangePasswordUsingToken(ChangePasswordUsingTokenDto request, CancellationToken cancellationToken)
        {
            AppUser? appUser = await userManager.FindByEmailAsync(request.Email);
            if (appUser == null)
                return BadRequest(new { Message = "Kullanıcı bulanamadı" });

          IdentityResult result =   await userManager.ResetPasswordAsync(appUser,request.Token,request.NewPassword);
            if (!result.Succeeded) return BadRequest(result.Errors.Select(s => s.Description));

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto request,CancellationToken cancellationToken)
        {
            AppUser? appUser = //await userManager.FindByEmailAsync(request.EmailOrUserName);
                               // await userManager.FindByNameAsync(request.EmailOrUserName);
           await userManager.Users.FirstOrDefaultAsync(u => u.Email == request.EmailOrUserName || u.UserName == request.EmailOrUserName, cancellationToken);

            if (appUser == null)
                return BadRequest(new { Message = "Kullanıcı bulanamadı" });

          bool result =  await userManager.CheckPasswordAsync(appUser, request.Password);
            if (!result) return BadRequest(new {Message = "Error password."});


            if (!appUser.EmailConfirmed) return BadRequest(new { Message = "Email onaylanmadı" });

            return Ok(new { Token = "Token" });



        }

        [HttpPost]

        public async Task<IActionResult> LoginWithSigning(LoginDto request,CancellationToken cancellationToken)
        {
            AppUser? appUser = await userManager.Users.FirstOrDefaultAsync(u=>u.Email == request.EmailOrUserName || u.UserName == request.EmailOrUserName,cancellationToken);

            if (appUser == null)
                return BadRequest(new { Message = "Kullanıcı bulanamadı" });

          SignInResult result = await signInManager.CheckPasswordSignInAsync(appUser, request.Password, true);

            if (result.IsLockedOut)
            {
                TimeSpan? timeSpan = appUser.LockoutEnd - DateTime.Now;

                if(timeSpan is not null)
                {
                    return StatusCode(500, "Şifrenizi 3 kez yanlış girdiniz." +
                        $"{timeSpan.Value.TotalSeconds} sonra deneyiniz.");
                }  else
                {
                    return StatusCode(500, "30 saniye sonra tekrar deneyiniz");
                }
            }
            if (!result.Succeeded)
            {
                return StatusCode(500, "Şifreniz yanlış");
            }

            if (result.IsNotAllowed)
            {
                return StatusCode(500, "Email adresiniz onaylı değil. lütfen onaylayınız.");
            }

           

            return Ok(new {Token="Token"});
            
        
        }
    
    }
}
