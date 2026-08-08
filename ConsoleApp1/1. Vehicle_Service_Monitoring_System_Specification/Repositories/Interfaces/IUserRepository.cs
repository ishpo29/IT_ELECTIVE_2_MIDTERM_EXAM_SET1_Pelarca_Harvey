using VehicleServiceMonitoringSystem.Models.Entities;

namespace VehicleServiceMonitoringSystem.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User? GetByUsername(string username);
        bool UsernameExists(string username);
        bool EmailExists(string email);
        User Add(User user);
    }
}
