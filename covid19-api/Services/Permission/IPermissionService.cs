namespace covid19_api.Services.Permission
{
    public interface IPermissionService
    {
        Task<List<int>> GetPermissionsAsync(int memberId);
    }
}
