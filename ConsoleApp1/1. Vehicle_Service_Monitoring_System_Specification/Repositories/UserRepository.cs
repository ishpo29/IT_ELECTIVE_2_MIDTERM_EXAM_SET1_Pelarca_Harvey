using VehicleServiceMonitoringSystem.Models.Entities;
using VehicleServiceMonitoringSystem.Repositories.Interfaces;

namespace VehicleServiceMonitoringSystem.Repositories
{
    // In-memory repository. Registered as a Singleton in Program.cs so the
    // static-like list survives across requests without an actual database.
    public class UserRepository : IUserRepository
    {
        private static readonly List<User> _users = new();
        private static int _nextId = 1;

        public User? GetByUsername(string username)
        {
            return _users.FirstOrDefault(u =>
                u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }

        public bool UsernameExists(string username)
        {
            return _users.Any(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }

        public bool EmailExists(string email)
        {
            return _users.Any(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public User Add(User user)
        {
            user.Id = _nextId++;
            _users.Add(user);
            return user;
        }
    }
}
