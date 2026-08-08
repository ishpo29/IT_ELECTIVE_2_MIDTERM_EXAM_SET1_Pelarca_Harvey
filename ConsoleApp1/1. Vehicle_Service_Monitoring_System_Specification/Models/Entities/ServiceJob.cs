namespace VehicleServiceMonitoringSystem.Models.Entities
{
    public class ServiceJob
    {
        public int Id { get; set; }
        public string ServiceNumber { get; set; } = string.Empty;

        // Customer Information
        public string CustomerName { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;

        // Vehicle Information
        public string VehicleMake { get; set; } = string.Empty;
        public string VehicleModel { get; set; } = string.Empty;
        public int ModelYear { get; set; }
        public string PlateNumber { get; set; } = string.Empty;
        public string VehicleColor { get; set; } = string.Empty;

        // Service Information
        public string ServiceType { get; set; } = string.Empty;
        public string ServiceBay { get; set; } = string.Empty;
        public DateTime CheckInDateTime { get; set; }
        public DateTime ExpectedReleaseDate { get; set; }
        public DateTime? ActualReleaseDateTime { get; set; }

        public ServiceStatus Status { get; set; } = ServiceStatus.Waiting;

        // Additional Information
        public string? Remarks { get; set; }
    }
}
