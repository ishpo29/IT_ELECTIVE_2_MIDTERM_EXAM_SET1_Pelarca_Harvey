namespace VehicleServiceMonitoringSystem.Models.DTOs
{
    public class ServiceJobReleaseDto
    {
        public int Id { get; set; }
        public string ServiceNumber { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string VehicleMake { get; set; } = string.Empty;
        public string VehicleModel { get; set; } = string.Empty;
        public string PlateNumber { get; set; } = string.Empty;
        public string ServiceType { get; set; } = string.Empty;
    }
}
