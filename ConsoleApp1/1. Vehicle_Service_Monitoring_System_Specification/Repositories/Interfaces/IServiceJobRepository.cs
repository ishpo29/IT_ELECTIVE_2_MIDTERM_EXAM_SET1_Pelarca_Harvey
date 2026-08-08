using VehicleServiceMonitoringSystem.Models.Entities;

namespace VehicleServiceMonitoringSystem.Repositories.Interfaces
{
    public interface IServiceJobRepository
    {
        List<ServiceJob> GetAll();
        ServiceJob? GetById(int id);
        ServiceJob Add(ServiceJob job);
        void Update(ServiceJob job);
        List<ServiceJob> Search(string? keyword);
        string GenerateNextServiceNumber();
    }
}
