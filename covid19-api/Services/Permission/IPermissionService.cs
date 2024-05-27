using covid19_api.Dtos.RolePermission;

namespace covid19_api.Services.Permission
{
    public interface IPermissionService
    {
        Task<ServiceResponse<List<GetRolePermissionDto>>> GetAllPermissions();

        Task<ServiceResponse<List<GetRolePermissionDto>>> AddRolePermissions(AddRolePermissionDto roleData);

        Task<ServiceResponse<List<GetRolePermissionDto>>> DeleteRolePermissions(DeleteRolePermissionDto data);
    }
}
