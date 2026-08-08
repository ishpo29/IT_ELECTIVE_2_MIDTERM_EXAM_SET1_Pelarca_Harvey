namespace VehicleServiceMonitoringSystem.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;

        // NOTE: For a lab exercise, the password is stored as a hash produced
        // by a simple one-way hash (see AccountController). In a real system
        // use ASP.NET Core Identity + a proper password hasher.
        public string PasswordHash { get; set; } = string.Empty;
    }
}
