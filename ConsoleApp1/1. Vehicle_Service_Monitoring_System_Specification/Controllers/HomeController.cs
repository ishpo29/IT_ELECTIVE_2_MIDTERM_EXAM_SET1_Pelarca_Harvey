using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleServiceMonitoringSystem.Models.Entities;
using VehicleServiceMonitoringSystem.Repositories.Interfaces;

namespace VehicleServiceMonitoringSystem.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IServiceJobRepository _serviceJobRepository;

        public HomeController(IServiceJobRepository serviceJobRepository)
        {
            _serviceJobRepository = serviceJobRepository;
        }

        // GET: /Home/Index  (Dashboard)
        public IActionResult Index()
        {
            var jobs = _serviceJobRepository.GetAll();

            ViewBag.TotalServices = jobs.Count;
            ViewBag.Waiting = jobs.Count(j => j.Status == ServiceStatus.Waiting);
            ViewBag.InService = jobs.Count(j => j.Status == ServiceStatus.InService);
            ViewBag.ReadyForRelease = jobs.Count(j => j.Status == ServiceStatus.ReadyForRelease);
            ViewBag.Released = jobs.Count(j => j.Status == ServiceStatus.Released);

            // Show the most recent jobs on the dashboard
            return View(jobs.Take(5).ToList());
        }

        [AllowAnonymous]
        public IActionResult Error()
        {
            return View();
        }
    }
}
