namespace EFCore.Identity.Learn.Dtos
{
    public sealed record LoginDto(
        
        string EmailOrUserName,
        string Password
        
        );
}
