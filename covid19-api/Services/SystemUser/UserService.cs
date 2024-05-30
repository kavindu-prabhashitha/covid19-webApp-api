using AutoMapper;
using covid19_api.Dtos.User;
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

        public Task<ServiceResponse<GetUserDto>> GetUserById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
