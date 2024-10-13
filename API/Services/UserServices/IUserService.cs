using API.Services.UserServices.Models;
using API.Shared.SharedModels;

namespace API.Services.UserServices
{
    public interface IUserService
    {
        Task<ResultModel> CreateNewUserAsync(UserRegistrationRequestModel model);
        Task<UserListDetailsResponseModel> GetUserListAsync();
        Task<UserDetailsResponseModel?> GetUserAsync(int userId);
        Task<ResultModel> UpdateUserAsync(int userId, UpdateUserRequestModel model);
        Task<ResultModel> DeleteUserAsync(int userId);
    }
}
