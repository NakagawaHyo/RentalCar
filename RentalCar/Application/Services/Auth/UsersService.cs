using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using RentalCar.Application.Database;
using RentalCar.Application.Models;
using RentalCar.Application.Startup;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace RentalCar.Application.Services.Auth
{
    public class UsersService
    {
        private readonly DatabaseContext _db;

        public UsersService(DatabaseContext db)
        {
            _db = db;
        }

        public async Task<User?> FindUserAsync(int userId)
        {
            return await _db.Users.FindAsync(userId);
        }

        public async Task<User?> FindUserAsync(string username, string password)
        {
            return await _db.Users.FirstOrDefaultAsync(x => x.LoginId == username && x.Password == password);
        }

        public async Task<User?> FindUserByLoginIdAsync(string loginId)
        {
            return await _db.Users.FirstOrDefaultAsync(x => x.LoginId == loginId);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            var addedUser = await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();

            await _db.UserRoles.AddAsync(new UserRole { RoleId = 1, User = user });
            await _db.SaveChangesAsync();
            return addedUser.Entity;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _db.Users
                    .ToListAsync();
        }

        public string GetSha256Hash(string input)
        {
            using (var hashAlgorithm = SHA256.Create())
            {
                var byteValue = Encoding.UTF8.GetBytes(input);
                var byteHash = hashAlgorithm.ComputeHash(byteValue);
                return Convert.ToBase64String(byteHash);
            }
        }
    }
}
