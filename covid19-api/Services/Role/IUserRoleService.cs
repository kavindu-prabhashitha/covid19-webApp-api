using covid19_api.Dtos.UserRole;
using covid19_api.Models;

namespace covid19_api.Services.Role
{
    public interface IUserRoleService
    {
        Task<ServiceResponse<List<GetUserRoleDto>>> GetAllRoles();

        Task<ServiceResponse<List<UserRole>>> AddUserRole(AddUserRoleDto roleData);

        Task<ServiceResponse<GetUserRoleDto>> AddPermissionsForUserRole(AddPermissionsToUserRoleDto permissionData);

        Task<ServiceResponse<GetUserRoleDto>> GetRoleById(int roleId);

        Task<ServiceResponse<List<GetUserRoleDto>>> UpdateRole(UpdateRoleDto data);

        Task<ServiceResponse<GetUserRoleDto>> RemovePermissionsFromUserRole(RemovePermissionFromUserRoleDto data);
    }
}
