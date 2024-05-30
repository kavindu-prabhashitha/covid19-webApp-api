using covid19_api.Dtos.User;

namespace covid19_api.Services.SystemUser
{
    public interface IUserService
    {
        Task<ServiceResponse<List<GetUserDto>>> GetAllUsers();
        Task<ServiceResponse<GetUserDto>> GetUserById(int id);
    }
}
