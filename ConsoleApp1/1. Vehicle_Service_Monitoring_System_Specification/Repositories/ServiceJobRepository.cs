using VehicleServiceMonitoringSystem.Models.Entities;
using VehicleServiceMonitoringSystem.Repositories.Interfaces;

namespace VehicleServiceMonitoringSystem.Repositories
{
    // In-memory repository. Registered as a Singleton in Program.cs.
    public class ServiceJobRepository : IServiceJobRepository
    {
        private static readonly List<ServiceJob> _jobs = new();
        private static int _nextId = 1;
        private static int _nextServiceNumber = 1;

        static ServiceJobRepository()
        {
            // Seed a few sample jobs so the dashboard/monitoring pages are
            // not empty the first time the app runs.
            Seed();
        }

        public List<ServiceJob> GetAll() => _jobs.OrderByDescending(j => j.CheckInDateTime).ToList();

        public ServiceJob? GetById(int id) => _jobs.FirstOrDefault(j => j.Id == id);

        public ServiceJob Add(ServiceJob job)
        {
            job.Id = _nextId++;
            job.ServiceNumber = GenerateNextServiceNumber();
            _jobs.Add(job);
            return job;
        }

        public void Update(ServiceJob job)
        {
            var existing = GetById(job.Id);
            if (existing is null) return;

            existing.CustomerName = job.CustomerName;
            existing.ContactNumber = job.ContactNumber;
            existing.VehicleMake = job.VehicleMake;
            existing.VehicleModel = job.VehicleModel;
            existing.ModelYear = job.ModelYear;
            existing.PlateNumber = job.PlateNumber;
            existing.VehicleColor = job.VehicleColor;
            existing.ServiceType = job.ServiceType;
            existing.ServiceBay = job.ServiceBay;
            existing.CheckInDateTime = job.CheckInDateTime;
            existing.ExpectedReleaseDate = job.ExpectedReleaseDate;
            existing.Status = job.Status;
            existing.Remarks = job.Remarks;
            existing.ActualReleaseDateTime = job.ActualReleaseDateTime;
        }

        public List<ServiceJob> Search(string? keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return GetAll();

            keyword = keyword.Trim();

            return _jobs.Where(j =>
                    j.ServiceNumber.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                    j.CustomerName.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                    j.PlateNumber.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                    $"{j.VehicleMake} {j.VehicleModel}".Contains(keyword, StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(j => j.CheckInDateTime)
                .ToList();
        }

        public string GenerateNextServiceNumber()
        {
            return $"SV-{_nextServiceNumber++:D4}";
        }

        private static void Seed()
        {
            _jobs.Add(new ServiceJob
            {
                Id = _nextId++,
                ServiceNumber = $"SV-{_nextServiceNumber++:D4}",
                CustomerName = "Juan Dela Cruz",
                ContactNumber = "0917-123-4567",
                VehicleMake = "Toyota",
                VehicleModel = "Vios",
                ModelYear = 2021,
                PlateNumber = "ABC-1234",
                VehicleColor = "White",
                ServiceType = "Oil Change",
                ServiceBay = "Bay 2",
                CheckInDateTime = DateTime.Today.AddHours(8).AddMinutes(30),
                ExpectedReleaseDate = DateTime.Today.AddHours(15),
                Status = ServiceStatus.InService,
                Remarks = "Customer requested synthetic oil."
            });

            _jobs.Add(new ServiceJob
            {
                Id = _nextId++,
                ServiceNumber = $"SV-{_nextServiceNumber++:D4}",
                CustomerName = "Maria Santos",
                ContactNumber = "0918-555-2211",
                VehicleMake = "Honda",
                VehicleModel = "Civic",
                ModelYear = 2019,
                PlateNumber = "XYZ-5678",
                VehicleColor = "Silver",
                ServiceType = "Brake Repair",
                ServiceBay = "Bay 1",
                CheckInDateTime = DateTime.Today.AddHours(9),
                ExpectedReleaseDate = DateTime.Today.AddHours(13),
                Status = ServiceStatus.Waiting,
                Remarks = null
            });

            _jobs.Add(new ServiceJob
            {
                Id = _nextId++,
                ServiceNumber = $"SV-{_nextServiceNumber++:D4}",
                CustomerName = "Pedro Reyes",
                ContactNumber = "0920-777-8899",
                VehicleMake = "Mitsubishi",
                VehicleModel = "Mirage",
                ModelYear = 2020,
                PlateNumber = "DEF-9012",
                VehicleColor = "Red",
                ServiceType = "Tire Replacement",
                ServiceBay = "Bay 3",
                CheckInDateTime = DateTime.Today.AddHours(7).AddMinutes(45),
                ExpectedReleaseDate = DateTime.Today.AddHours(10),
                Status = ServiceStatus.ReadyForRelease,
                Remarks = "All four tires replaced."
            });

            _jobs.Add(new ServiceJob
            {
                Id = _nextId++,
                ServiceNumber = $"SV-{_nextServiceNumber++:D4}",
                CustomerName = "Ana Lopez",
                ContactNumber = "0915-333-4455",
                VehicleMake = "Ford",
                VehicleModel = "Ranger",
                ModelYear = 2018,
                PlateNumber = "GHI-3456",
                VehicleColor = "Blue",
                ServiceType = "General Check-up",
                ServiceBay = "Bay 1",
                CheckInDateTime = DateTime.Today.AddHours(6).AddMinutes(30),
                ExpectedReleaseDate = DateTime.Today.AddHours(9),
                ActualReleaseDateTime = DateTime.Today.AddHours(8).AddMinutes(50),
                Status = ServiceStatus.Released,
                Remarks = "No issues found."
            });
        }
    }
}
