namespace EFCore.Identity.Learn.Dtos
{
    public sealed record ChangePasswordDto(
        
        Guid id,
        string CurrentPassword,
        string NewPassword
        
        );
}
