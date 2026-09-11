using DVLD.Application.DTOs;
using DVLD.Application.Interfaces;
using DVLD.Domain.Entities;

namespace DVLD.Application.Services
{
    public class UsersService : IUserService
    {
        private readonly IUsersRepository _userRepo;


        public UsersService(IUsersRepository usersRepository)
        {
            _userRepo = usersRepository;
        }


        public async Task<UserDto?> GetByIdAsync(int userId)
        {
            var user = await _userRepo.GetByIdAsync(userId);

            if (user == null)
                return null;

            return MapToDto(user);
        }


        public async Task<UserDto?> GetByPersonIdAsync(int personId)
        {
            var user = await _userRepo.GetByPersonIdAsync(personId);

            if (user == null)
                return null;

            return MapToDto(user);
        }


        public async Task<UserDto?> GetByUsernameAsync(string username)
        {
            var allUsers = await _userRepo.GetAllAsync();

            var user = allUsers
                .FirstOrDefault(u => u.UserName == username);

            if (user == null)
                return null;

            return MapToDto(user);
        }


        public async Task<UserDto?> LoginAsync(
            string username,
            string password)
        {
            var user =
                await _userRepo.GetByUsernameAndPasswordAsync(
                    username,
                    password);

            if (user == null)
                return null;
            return MapToDto(user);
        }


        public async Task<List<UserDto>> GetAllAsync()
        {
            var users = await _userRepo.GetAllAsync();

            return users
                .Select(MapToDto)
                .ToList();
        }


        public async Task<int> CreateAsync(
            CreateUserRequest request)
        {
            // Check if Person exists
            var personExists =
                await _userRepo.ExistsByPersonIdAsync(
                    request.PersonId);

            if (!personExists)
                throw new KeyNotFoundException(
                    $"Person with ID {request.PersonId} was not found.");


            // Check if username already exists
            var usernameExists =
                await _userRepo.UsernameExistsAsync(
                    request.UserName);

            if (usernameExists)
                throw new InvalidOperationException(
                    $"Username '{request.UserName}' already exists.");


            try
            {
                // Create Domain User
                var user = new User(
                    request.PersonId,
                    request.UserName,
                    request.Password,
                    request.IsActive,
                    null!);

                // Save
                await _userRepo.AddAsync(user);

                return user.UserId;
            }
            catch (ArgumentException ex)
            {
                throw new InvalidOperationException(
                    $"Failed to create user: {ex.Message}",
                    ex);
            }
        }


        public async Task<bool> UpdateAsync(
            int userId,
            UpdateUserRequest request)
        {
            var user =
                await _userRepo.GetByIdAsync(userId);

            if (user == null)
                return false;


            try
            {
                // Domain handles validation + modification
                user.UpdateInformation(
                    request.UserName,
                    request.IsActive);

                // Repository handles persistence
                return await _userRepo.UpdateAsync(user);
            }
            catch (ArgumentException)
            {
                return false;
            }
        }


        public async Task<bool> ChangePasswordAsync(
            int userId,
            ChangePasswordRequest request)
        {
            var user =
                await _userRepo.GetByIdAsync(userId);

            if (user == null)
                return false;


            try
            {
                // Domain handles password rules
                user.ChangePassword(
                    request.NewPassword);

                // Repository saves the changed password
                return await _userRepo.ChangePasswordAsync(
                    user.UserId,
                    user.Password);
            }
            catch (ArgumentException)
            {
                return false;
            }
        }


        public async Task<bool> IsExist(int userId)
        {
            var user =
                await _userRepo.GetByIdAsync(userId);

            return user != null;
        }


        public async Task<bool> DeleteAsync(int userId)
        {
            var user =
                await _userRepo.GetByIdAsync(userId);

            if (user == null)
                return false;

            return await _userRepo.DeleteAsync(userId);
        }


        private static UserDto MapToDto(User user)
        {
            return new UserDto
            {
                UserId = user.UserId,
                PersonId = user.PersonId,
                UserName = user.UserName,
                IsActive = user.IsActive,
                Person = user.Person
            };
        }
    }
}