using AutoMapper;
using covid19_api.Dtos.User;
using covid19_api.Models;
using Microsoft.EntityFrameworkCore;

namespace covid19_api.Services.SystemUser
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UserService(DataContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<GetUserDto>>> GetAllUsers()
        {
            var response = new ServiceResponse<List<GetUserDto>>();

            var users = await _context.Users.Include(x => x.Role).ToListAsync();

            if (users is not null)
            {
                response.Data = users.Select(x=> _mapper.Map<GetUserDto>(x)).ToList();
                response.Message = "All Users in the System";
                return response;
            }

            response.Success = false;
            response.Message = "Operation Failed";

            return response;
        }

        public async Task<ServiceResponse<GetUserDto>> GetUserById(int id)
        {
            var response = new ServiceResponse<GetUserDto>();

            var user = await _context.Users
                .Include(x => x.Role)
                .ThenInclude(x => x.RolePermissions)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (user is not null)
            {
                response.Data =  _mapper.Map<GetUserDto>(user);
                response.Message = "All Users in the System";
                return response;
            }

            response.Success = false;
            response.Message = "Operation Failed";

            return response;
        }

        public async Task<ServiceResponse<List<int>>> GetPermissionsByUserId(int id)
        {
            var response = new ServiceResponse<List<int>>();

            var user = await _context.Users
                .Include(x => x.Role)
                .ThenInclude(x => x.RolePermissions)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (user?.Role?.RolePermissions is not null)
            {
                var userPermissions = user?.Role?.RolePermissions?.Select(x => x.RPid).ToList();
                response.Data = userPermissions;
                response.Message = "All Permissons related to user in the System";
                return response;
            }

            response.Success = false;
            response.Data = new List<int>();
            response.Message = "Operation Failed";

            return response;
        }
    }
}
