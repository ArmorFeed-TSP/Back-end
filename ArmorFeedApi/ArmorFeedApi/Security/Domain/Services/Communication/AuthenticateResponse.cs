namespace ArmorFeedApi.Security.Domain.Services.Communication;

public class AuthenticateResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public byte[] Photo { get; set; }
    public string Ruc { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
}