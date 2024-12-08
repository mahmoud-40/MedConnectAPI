namespace Medical.DTOs.Account;
public class AuthDTO
{
    public string? Message { get; set; }
    public bool IsAuthenticated { get; set; }
    public string? Token { get; set; }
}
