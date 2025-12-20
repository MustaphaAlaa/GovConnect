namespace ModelDTO.API;

public class LoginResponseDTO
{
    public string Token { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public bool IsAuthorize { get; set; }
    public List<string> Roles { get; set; }

}
