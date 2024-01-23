namespace BookingSystem.Identity.Application.Models;

public class SignUpModel
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string FullName { get; set; }
}
