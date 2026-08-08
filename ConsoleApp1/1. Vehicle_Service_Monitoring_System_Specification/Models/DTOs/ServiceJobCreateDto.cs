using System.ComponentModel.DataAnnotations;

namespace VehicleServiceMonitoringSystem.Models.DTOs
{
    public class ServiceJobCreateDto
    {
        // Section 1 - Customer Information
        [Required(ErrorMessage = "Customer name is required.")]
        [StringLength(100)]
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Contact number is required.")]
        [Phone(ErrorMessage = "Enter a valid contact number.")]
        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; } = string.Empty;

        // Section 2 - Vehicle Information
        [Required(ErrorMessage = "Vehicle make is required.")]
        [StringLength(50)]
        [Display(Name = "Vehicle Make")]
        public string VehicleMake { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vehicle model is required.")]
        [StringLength(50)]
        [Display(Name = "Vehicle Model")]
        public string VehicleModel { get; set; } = string.Empty;

        [Required(ErrorMessage = "Model year is required.")]
        [Range(1980, 2100, ErrorMessage = "Enter a valid model year.")]
        [Display(Name = "Model Year")]
        public int ModelYear { get; set; }

        [Required(ErrorMessage = "Plate number is required.")]
        [StringLength(15)]
        [Display(Name = "Plate Number")]
        public string PlateNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vehicle color is required.")]
        [StringLength(30)]
        [Display(Name = "Vehicle Color")]
        public string VehicleColor { get; set; } = string.Empty;

        // Section 3 - Service Information
        [Required(ErrorMessage = "Service type is required.")]
        [StringLength(50)]
        [Display(Name = "Service Type")]
        public string ServiceType { get; set; } = string.Empty;

        [Required(ErrorMessage = "Service bay is required.")]
        [StringLength(30)]
        [Display(Name = "Service Bay")]
        public string ServiceBay { get; set; } = string.Empty;

        [Required(ErrorMessage = "Check-in date and time is required.")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Check-in Date & Time")]
        public DateTime CheckInDateTime { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Expected release date is required.")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Expected Release Date")]
        public DateTime ExpectedReleaseDate { get; set; } = DateTime.Now.AddHours(4);

        // Section 4 - Additional Information
        [StringLength(500)]
        [Display(Name = "Remarks")]
        public string? Remarks { get; set; }
    }
}
