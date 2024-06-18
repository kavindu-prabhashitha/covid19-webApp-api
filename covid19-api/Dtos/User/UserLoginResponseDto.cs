namespace covid19_api.Dtos.User
{
    public class UserLoginResponseDto
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public int UserId { get; set; } 
    }
}
