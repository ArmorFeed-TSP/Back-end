namespace ArmorFeedApi.Security.Resources;

public class UserResource
{
    public int Id { get; set; }
    public string Name { get; set; }
    public byte[] Photo { get; set; }
    public string Ruc { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
}