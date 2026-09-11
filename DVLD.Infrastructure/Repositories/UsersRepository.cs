using DVLD.Application.DTOs;
using DVLD.Application.Interfaces;
using DVLD.Domain.Entities;
using DVLD.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD.Infrastructure.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private DvldDbContext _Context;
        public UsersRepository(DvldDbContext context)
        {
            _Context = context;
        }
       public async Task AddAsync(User entity)
        {
            var user = MapToEntity(entity);

            await _Context.Users.AddAsync(user);

            await _Context.SaveChangesAsync();
        }

       public async Task<int> CountAsync()
        {
            return await _Context.Users.AsNoTracking().CountAsync();
        }
        
        public async Task<bool> DeleteAsync(int Id)
        {
            var user = await _Context.Users.FirstOrDefaultAsync(i => i.UserId == Id);
            if (user == null)
            {
                return false;
            }
            _Context.Remove(user);
            
             return await _Context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistsByPersonIdAsync(int personId)
        {  
            return await _Context.Users.AsNoTracking().AnyAsync(u => u.PersonId == personId);
        }
        public async Task<User?> GetByUsernameAndPasswordAsync(
    string username,
    string password)
        {
            var entity = await _Context.Users
                .AsNoTracking()
                .Include(u => u.Person)
                .FirstOrDefaultAsync(
                    u => u.UserName == username &&
                         u.Password == password);

            return entity == null
                ? null
                : MapToDomain(entity);
        }
        public async Task<List<User>> GetAllAsync()
        {
            var entities = await _Context.Users
                .AsNoTracking()
                .ToListAsync();

            return entities
                .Select(MapToDomain)
                .ToList();
        }
        public async Task<bool>ChangePasswordAsync(int userId, string newPassword)
        {
            var user = await _Context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            if (user == null)
            {
                return false;
            }
            user.Password = newPassword;
            return await _Context.SaveChangesAsync() > 0;
        }
        public async Task<List<User>> GetByActiveStateAsync(bool isActive)
        {
            var entities = await _Context.Users
                .AsNoTracking()
                .Where(u => u.IsActive == isActive)
                .ToListAsync();

            return entities.Select(MapToDomain).ToList();
        }
        public async Task<User?> GetByIdAsync(int userId)
        {
            var entity = await _Context.Users
                .AsNoTracking()
                .Include(u => u.Person)
                .FirstOrDefaultAsync(u => u.UserId == userId);

            return entity == null
                ? null
                : MapToDomain(entity);
        }

        public async Task<User?> GetByPersonIdAsync(int personId)
        {
            var entity = await _Context.Users
                .AsNoTracking()
                .Include(u => u.Person)
                .FirstOrDefaultAsync(u => u.PersonId == personId);

            return entity == null
                ? null
                : MapToDomain(entity);
        }

       

        public async Task<bool> UpdateAsync(User entity)
        {
            var user = await _Context.Users.FirstOrDefaultAsync(u => u.UserId == entity.UserId);

            if (user == null)
                throw new KeyNotFoundException($"User with ID {entity.UserId} was not found.");

            user.UserName = entity.UserName;
            user.Password = entity.Password;
            user.IsActive = entity.IsActive;

           return await _Context.SaveChangesAsync()>0;

        }

        public async Task ChangeActivison(int Personid,bool status)
        {
            var user=await _Context.Users.FirstOrDefaultAsync(i=>i.UserId == Personid);
            if (user == null)return;
            user.IsActive=status;
            
            await _Context.SaveChangesAsync();
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            return await _Context.Users.AsNoTracking().AnyAsync(u => u.UserName == username);
        }

        private static User MapToDomain(
            Infrastructure.Entities.User entity)
        {
            var User = new User(
                    entity.UserId,
                    entity.PersonId,
                    entity.UserName,
                    entity.Password,
                    entity.IsActive
                );

            return User;
        }

     
        private static Infrastructure.Entities.User MapToEntity(
                User user)
        {
            return new Infrastructure.Entities.User
            {
                PersonId = user.PersonId,
                UserName = user.UserName,
                Password = user.Password,
                IsActive = user.IsActive
            };
        }


      
    }
}
