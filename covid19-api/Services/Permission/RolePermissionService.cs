using AutoMapper;
using covid19_api.Dtos.RolePermission;
using covid19_api.Models;
using Microsoft.EntityFrameworkCore;

namespace covid19_api.Services.Permission
{
    public class RolePermissionService : IRolePermissionService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public RolePermissionService(DataContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }



        public async Task<ServiceResponse<List<GetRolePermissionDto>>> GetAllPermissions()
        {
            var response = new ServiceResponse<List<GetRolePermissionDto>>();
            var permissionList = new List<GetRolePermissionDto>();
            var dbData =await  _context.RolePermissions.Select(p => _mapper.Map<GetRolePermissionDto>(p)).ToListAsync();
               
            if(dbData != null) {
                response.Data = dbData;
                return response;
            }
          
            response.Success = false;
            return response;
        }

        public async Task<ServiceResponse<List<GetRolePermissionDto>>> AddRolePermissions(AddRolePermissionDto roleData)
        {
            var response = new ServiceResponse<List<GetRolePermissionDto>>();
            if(roleData == null)
            {
                response.Success = false;
                response.Message = "Role Permission Added Failed";
            }

            _context.RolePermissions.Add(_mapper.Map<RolePermission>(roleData));
            await _context.SaveChangesAsync();
            response.Message = "Role Permission Added";
            return response;
        }

        public async Task<ServiceResponse<List<GetRolePermissionDto>>> DeleteRolePermissions(DeleteRolePermissionDto data)
        {
            var response = new ServiceResponse<List<GetRolePermissionDto>>();
            try
            {
                var permission = await _context.RolePermissions
                    .FirstOrDefaultAsync(p => p.Id == data.PermissionId);
                if (permission is null)
                {
                    throw new Exception($"Permission with Id '{data.PermissionId}' not found");
                }

                _context.RolePermissions.Remove(permission);
                await _context.SaveChangesAsync();
                response.Data = await _context.RolePermissions.Select(r => _mapper.Map<GetRolePermissionDto>(r)).ToListAsync();
                    

            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
