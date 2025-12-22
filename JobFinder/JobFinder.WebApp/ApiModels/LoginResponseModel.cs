public class LoginResponseModel
{
    public string Token { get; set; }
    public UserModel User { get; set; }
}

public class UserModel
{
    public int IDUser { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
}
