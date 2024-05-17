using covid19_api.Models;

namespace covid19_api.Services.Role
{
    public interface IUserRoleService
    {
        Task<ServiceResponse<List<UserRole>>> GetAllRoles();
    }
}
