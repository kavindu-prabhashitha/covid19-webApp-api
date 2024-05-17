using Microsoft.EntityFrameworkCore;

namespace covid19_api.Services.Role
{
    public class UserRoleService : IUserRoleService
    {
        private readonly DataContext _context;

        public UserRoleService(DataContext context)
        {
            _context = context;
        }


        public async Task<ServiceResponse<List<UserRole>>> GetAllRoles()
        {
            var response = new ServiceResponse<List<UserRole>>();
            var roles = await _context.UserRole.ToListAsync();

            response.Data = roles;
            return response;
        }
    }
}
