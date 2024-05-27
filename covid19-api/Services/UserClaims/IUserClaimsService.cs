namespace covid19_api.Services.UserClaims
{
    public interface IUserClaimsService
    {
        string GetUserName();

        string GetUserRole();

        string GetUserId();
    }
}
