using API.Data;
using API.Models.Users;
using API.Services.UserServices.Models;
using API.Shared.SharedModels;
using API.Shared.Utils;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResultModel> CreateNewUserAsync(UserRegistrationRequestModel model)
        {
            var validateResult = ValidateUpSertUserDetailsModel(model.Email, model.PhoneNumber);
            if (validateResult.Success == false)
            {
                return validateResult;
            }

            try
            {
                var user = new User()
                {
                    FullName = model.FullName,
                    Email = model.Email,
                    DateOfBirth = model.DateOfBirth,
                    PhoneNumber = model.PhoneNumber,
                    ReferralType = model.ReferralType,
                    CreatedAt = DateTime.Now
                };

                _context.Add(user);
                await _context.SaveChangesAsync();

                return new ResultModel { Success = true };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new ResultModel { Success = false, Message = "User registration has been failed."};
            }
        }

        public async Task<UserDetailsResponseModel?> GetUserAsync(int userId)
        {
            var user = await _context.Users
                .Where(x => x.IsDeleted == false && x.Id == userId)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return null;
            }

            return _mapper.Map<UserDetailsResponseModel>(user);
        }

        public async Task<UserListDetailsResponseModel> GetUserListAsync()
        {
            var response = new UserListDetailsResponseModel();
            var users = await _context.Users.Where(x => x.IsDeleted == false).ToListAsync();

            if (users != null && users.Count != 0)
            {
                foreach (var user in users)
                {
                    response.Data.Add(_mapper.Map<UserDetailsResponseModel>(user));
                }
            }

            return response;
        }

        public async Task<ResultModel> UpdateUserAsync(int userId, UpdateUserRequestModel model)
        {
            var validateResult = ValidateUpSertUserDetailsModel(model.Email, model.PhoneNumber);
            if (validateResult.Success == false)
            {
                return validateResult;
            }

            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return new ResultModel() { Success = false, Message = $"User with id '{userId}' is not found." };
            }

            try
            {
                user.FullName = model.FullName;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                user.DateOfBirth = model.DateOfBirth;
                user.ReferralType = model.ReferralType;
                user.ModifiedAt = DateTime.Now;

                await _context.SaveChangesAsync();
                return new ResultModel() { Success = true };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new ResultModel() { Success = false, Message = "Updating user details has been failed." };
            }
        }

        public async Task<ResultModel> DeleteUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return new ResultModel() { Success = false, Message = $"User with id '{userId}' is not found." };
            }

            try
            {
                user.IsDeleted = true;
                user.DeletedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();
                return new ResultModel() { Success = true };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new ResultModel() { Success = false, Message = "Deleting user has been failed." };
            }
        }

        private ResultModel ValidateUpSertUserDetailsModel(string email, string phoneNumber)
        {
            if (!EmailValidator.IsValidEmail(email))
            {
                return new ResultModel { Success = false, Message = "Please enter a valid email address (xxxx@xxxx.xxx)." };
            }

            if (!PhoneNumberValidator.IsValidPhoneNumber(phoneNumber))
            {
                return new ResultModel { Success = false, Message = "Please enter a valid phone number (format: 01x-xxx xxxx or 01x xxxx xxxx)." };
            }

            return new ResultModel() { Success = true };
        }
    }
}
