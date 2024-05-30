using AutoMapper;
using covid19_api.Dtos.RolePermission;
using covid19_api.Dtos.UserRole;
using covid19_api.Models;
using Microsoft.EntityFrameworkCore;

namespace covid19_api.Services.Role
{
    public class UserRoleService : IUserRoleService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserRoleService(DataContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        public async Task<ServiceResponse<GetUserRoleDto>> AddPermissionsForUserRole(AddPermissionsToUserRoleDto permissionData)
        {
            var response = new ServiceResponse<GetUserRoleDto>();
            if (permissionData is null)
            {
                response.Success = false;
                response.Message = "Invalid Permission Data";
                return response;
            }

            var userRole = await _context.UserRoles
                .Include(r=> r.RolePermissions)
                .Where(r=> r.Id == permissionData.UserRoleId).FirstOrDefaultAsync();

            if(userRole is null)
            {
                response.Success = false;
                response.Message = "UserRole not found";
                return response;
            }


            foreach (var item in permissionData.PermissionIds)
            {
                var permission = await _context.RolePermissions.FirstOrDefaultAsync(r => r.Id == item);
                   
                if (permission is null)
                {
                    response.Success = false;
                    response.Message = "Permission not found";
                    return response;
                }  
                 userRole.RolePermissions!.Add(permission);
          
            }

            await _context.SaveChangesAsync();
            var userRoleUpdated = await _context.UserRoles
                .Include(r => r.RolePermissions)
                .Where(r => r.Id == permissionData.UserRoleId).FirstOrDefaultAsync();
            response.Data = _mapper.Map<GetUserRoleDto>(userRoleUpdated);
            response.Message = "use Role With Permissions";
            return response;
    
        }

        public async Task<ServiceResponse<List<UserRole>>> AddUserRole(AddUserRoleDto roleData)
        {
            var response = new ServiceResponse<List<UserRole>>();
            if(roleData != null)
            {
                _context.UserRoles.Add(_mapper.Map<UserRole>(roleData));
                await _context.SaveChangesAsync();
                var roles = await _context.UserRoles.ToListAsync();
                response.Data = roles;
                return response;
            }

            response.Data = [];
            response.Success = false;
            response.Message = "user Role Not Added";
            return response;
            
        }

        public async Task<ServiceResponse<List<GetUserRoleDto>>> GetAllRoles()
        {
            var response = new ServiceResponse<List<GetUserRoleDto>>();
            var roles = await _context.UserRoles
                .Include(r=> r.RolePermissions)
                .ToListAsync();

            response.Data = roles.Select(r=> _mapper.Map<GetUserRoleDto>(r)).ToList();
            return response;
        }

        public async Task<ServiceResponse<GetUserRoleDto>> GetRoleById(int roleId)
        {
            var response = new ServiceResponse<GetUserRoleDto>();
            var role = await _context.UserRoles
                .Include(r => r.RolePermissions)
                .Where(x => x.Id == roleId)
                .FirstOrDefaultAsync();

            response.Data = _mapper.Map<GetUserRoleDto>(role);
            return response;
        }

        public async Task<ServiceResponse<List<GetUserRoleDto>>> UpdateRole(UpdateRoleDto data)
        {
            var response = new ServiceResponse<List<GetUserRoleDto>>();
            if (data == null)
            {
                response.Success = false;
                response.Message = "Invalid Data Provided";
                return response;
            }

            var role = await _context.UserRoles.Where(x => x.Id == data.Id).FirstOrDefaultAsync();

            if (role is null)
            {
                response.Success = false;
                response.Message = "role Not Found";
                return response;
            }

            if (data.Name is not null) role.Name = data.Name;
            if (data.Description is not null) role.Description = data.Description;
            await _context.SaveChangesAsync();

            return response;
        }

    }
}
