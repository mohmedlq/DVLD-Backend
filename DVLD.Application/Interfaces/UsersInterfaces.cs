using DVLD.Domain.Entities;
using DVLD.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD.Application.Interfaces
{
    public interface IUsersRepository:ICrud<User>
    {
         Task<User?> GetByPersonIdAsync(int personId);

        Task<bool> ExistsByPersonIdAsync(int personId);

        Task<bool> UsernameExistsAsync(string username);

        Task<User?> GetByUsernameAndPasswordAsync(
            string username,
            string password);

        Task<List<User>> GetByActiveStateAsync(bool isActive);

        Task<int> CountAsync();

        Task<bool> ChangePasswordAsync(int userId,String pasword);


    }
    public interface IUserService
    {
        Task<UserDto?> GetByIdAsync(int userId);

        Task<UserDto?> GetByPersonIdAsync(int personId);

        Task<UserDto?> GetByUsernameAsync(string username);

        Task<UserDto?> LoginAsync(
            string username,
            string password);

        Task<List<UserDto>> GetAllAsync();

        Task<int> CreateAsync(
            CreateUserRequest request);

        Task<bool> UpdateAsync(
            int userId,
            UpdateUserRequest request);

        Task<bool> IsExist(
            int userId);
        Task<bool> ChangePasswordAsync(int userId, ChangePasswordRequest request);
        Task<bool> DeleteAsync(int userId);
    }
}
