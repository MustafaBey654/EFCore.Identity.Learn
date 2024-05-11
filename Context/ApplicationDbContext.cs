using EFCore.Identity.Learn.Controllers;
using EFCore.Identity.Learn.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Identity.Learn.Context
{
    public sealed class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, Guid,IdentityUserClaim<Guid>,AppUserRole,
        IdentityUserLogin<Guid>,
        IdentityRoleClaim<Guid>,IdentityUserToken<Guid>>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }


    }
}
