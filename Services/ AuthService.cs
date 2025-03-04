using AuthAPI.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace AuthAPI.Services
{
    public class AuthService
    {
        private readonly string filePath = "users.json";
        private List<User> _users;

        public AuthService()
        {
            _users = LoadUsers();
        }

        private List<User> LoadUsers()
        {
            if (File.Exists(filePath))
                return JsonSerializer.Deserialize<List<User>>(File.ReadAllText(filePath)) ?? new List<User>();

            return new List<User>();
        }

        private void SaveUsers()
        {
            File.WriteAllText(filePath, JsonSerializer.Serialize(_users));
        }

        public bool Register(User user)
        {
            if (_users.Any(u => u.Username == user.Username))
                return false;

            _users.Add(user);
            SaveUsers();
            return true;
        }

        public bool Login(string username, string password)
        {
            return _users.Any(u => u.Username == username && u.Password == password);
        }
    }
}
