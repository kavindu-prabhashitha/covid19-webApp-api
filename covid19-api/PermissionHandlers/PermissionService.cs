
using Microsoft.EntityFrameworkCore;

namespace covid19_api.PermissionHandlers
{
    public class PermissionService : IPermissionService
    {
        private readonly DataContext _context;

        public PermissionService(DataContext context)
        {
            _context = context;
        }
        public async Task<List<string>> GetPermissionsAsync(int memberId)
        {
            var userPermissionList = new List<string>();
            var userRolePermission =  await _context.Users
                  .Include(x => x.Role)
                  .ThenInclude(x => x.RolePermissions)
                  .Where(x => x.Id == memberId)
                  .Select(x => x.Role!.RolePermissions)
                  .FirstOrDefaultAsync();

            if(userRolePermission is not null)
            {
                userRolePermission.ForEach(userRolePermission =>
                {
                    userPermissionList.Add(userRolePermission.RPid);
                });
            }


            
            return userPermissionList;
        }
    }
}
