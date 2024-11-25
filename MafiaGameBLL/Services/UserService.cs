using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using MafiaGameDAL;
using MafiaGameDAL.Models;

namespace MafiaGameBLL.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public bool Register(string username, string password)
        {
            if (_context.Users.Any(u => u.Name == username))
                return false;

            var passwordHash = HashPassword(password);
            var user = new User { Name = username, PasswordHash = passwordHash };
            _context.Users.Add(user);
            _context.SaveChanges();
            return true;
        }

        public bool Login(string username, string password)
        {
            var passwordHash = HashPassword(password);
            return _context.Users.Any(u => u.Name == username && u.PasswordHash == passwordHash);
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
