namespace EFCore.Identity.Learn.Dtos
{
    public record RegisterDto(
        string Email, //Email zorunluluğu yok.
        string UserName,
        string FirstName,//Zorunlu değil
        string LastName,//Zorunlu değil
        string Password
        );
    
}
