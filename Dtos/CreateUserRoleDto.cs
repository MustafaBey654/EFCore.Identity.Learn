namespace EFCore.Identity.Learn.Dtos
{
    public sealed record CreateUserRoleDto(
        
        Guid UserId,
        Guid RoleId
        );
}
