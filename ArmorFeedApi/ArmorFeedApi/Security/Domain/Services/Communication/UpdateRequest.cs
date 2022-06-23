namespace ArmorFeedApi.Security.Domain.Services.Communication;

public class UpdateRequest
{
     public string Name { get; set; }
     public byte[] Photo { get; set; }
     public string Ruc { get; set; }
     public string PhoneNumber { get; set; }
     public string Email { get; set; }
     public string Password { get; set; }
}