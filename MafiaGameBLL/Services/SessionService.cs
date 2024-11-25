using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using MafiaGameDAL;
using MafiaGameDAL.Models;

namespace MafiaGameBLL.Services
{
    public class SessionService
    {
        private readonly AppDbContext _context;

        public SessionService(AppDbContext context)
        {
            _context = context;
        }

        public bool CreateSession(string name, string password, int userId)
        {
            if (_context.Sessions.Any(s => s.Name == name))
                return false;

            var passwordHash = HashPassword(password);
            var session = new Session { Name = name, PasswordHash = passwordHash, CreatorId = userId };
            _context.Sessions.Add(session);
            _context.SaveChanges();
            return true;
        }

        public IEnumerable<Session> GetSessions()
        {
            return _context.Sessions.ToList();
        }

        public bool JoinSession(string sessionName, string password, int userId)
        {
            var session = _context.Sessions.FirstOrDefault(s => s.Name == sessionName);

            // Перевіряємо, чи сесія існує і чи правильний пароль
            if (session == null || session.PasswordHash != HashPassword(password))
            {
                return false;
            }

            // Перевіряємо, чи користувач вже не в сесії
            var existingPlayer = _context.SessionPlayers.FirstOrDefault(sp => sp.SessionId == session.Id && sp.UserId == userId);

            if (existingPlayer != null)
            {
                return false; // Гравець вже в сесії
            }

            // Додаємо гравця до сесії
            var newSessionPlayer = new SessionPlayer
            {
                SessionId = session.Id,
                UserId = userId,
                IsReady = false // За замовчуванням новий гравець не готовий
            };

            _context.SessionPlayers.Add(newSessionPlayer);
            _context.SaveChanges(); // Зберігаємо зміни в базі даних

            return true;
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

        public bool DeleteSession(int sessionId, int userId)
        {
            var session = _context.Sessions.FirstOrDefault(s => s.Id == sessionId);

            if (session == null || session.CreatorId != userId)
                return false;

            _context.Sessions.Remove(session);
            _context.SaveChanges();
            return true;
        }

        public List<User> GetPlayersInSession(int sessionId)
        {
            // Отримуємо список користувачів, які є учасниками сесії
            return _context.SessionPlayers
                .Where(sp => sp.SessionId == sessionId)
                .Join(_context.Users,
                      sp => sp.UserId,   // З'єднуємо по UserId
                      u => u.Id,         // З'єднуємо по Id користувача
                      (sp, u) => u)      // Вибираємо користувача
                .ToList();
        }

        public bool SetPlayerReadyStatus(int sessionId, int userId, bool isReady)
        {
            var sessionPlayer = _context.SessionPlayers
                .FirstOrDefault(sp => sp.SessionId == sessionId && sp.UserId == userId);

            if (sessionPlayer == null)
                return false;

            sessionPlayer.IsReady = isReady;
            _context.SaveChanges();
            return true;
        }

        public bool LeaveSession(int sessionId, int userId)
        {
            var sessionPlayer = _context.SessionPlayers
                .FirstOrDefault(sp => sp.SessionId == sessionId && sp.UserId == userId);

            // Логування для перевірки
            if (sessionPlayer == null)
            {
                Console.WriteLine($"No record found for SessionId: {sessionId}, UserId: {userId}");
                return false;
            }

            // Якщо знайдений запис, видаляємо його
            _context.SessionPlayers.Remove(sessionPlayer);
            _context.SaveChanges();
            return true;
        }

        public bool AreAllPlayersReady(int sessionId)
        {
            // Отримуємо всіх гравців у сесії
            var sessionPlayers = _context.SessionPlayers
                                          .Where(sp => sp.SessionId == sessionId)
                                          .ToList();

            // Перевіряємо, чи всі гравці мають статус "готовий" (IsReady == true)
            return sessionPlayers.All(sp => sp.IsReady);
        }

        public bool IsPlayerInSession(int sessionId, int userId)
        {
            var sessionPlayer = _context.SessionPlayers
                .FirstOrDefault(sp => sp.SessionId == sessionId && sp.UserId == userId);

            return sessionPlayer != null;
        }
    }
}