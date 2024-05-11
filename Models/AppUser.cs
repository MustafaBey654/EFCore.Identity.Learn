using Microsoft.AspNetCore.Identity;

namespace EFCore.Identity.Learn.Models
{
    public sealed class AppUser:IdentityUser<Guid>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public string FullName => string.Join(" ", FirstName, LastName);

    }
}
