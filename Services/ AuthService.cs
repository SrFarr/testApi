using AuthAPI.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace AuthAPI.Services
{
    public class AuthService
    {
        private readonly string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "users.json");
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
            var json = JsonSerializer.Serialize(_users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

        public bool Register(User user)
        {
            if (_users.Any(u => u.Username == user.Username))
                return false; // Username sudah digunakan

            // Simpan password dalam bentuk plain text
            _users.Add(user);
            SaveUsers();
            return true;
        }

        public bool Login(string username, string password)
        {
            var user = _users.FirstOrDefault(u => u.Username == username);
            if (user == null)
                return false; // Username tidak ditemukan

            // Verifikasi password dalam bentuk plain text
            return user.Password == password;
        }
    }
}