namespace covid19_api.PermissionHandlers
{
    public interface IPermissionService
    {
        Task<List<string>> GetPermissionsAsync(int memberId);
    }
}
