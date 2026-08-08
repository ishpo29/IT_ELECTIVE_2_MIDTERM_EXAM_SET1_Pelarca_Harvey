using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleServiceMonitoringSystem.Models.DTOs;
using VehicleServiceMonitoringSystem.Models.Entities;
using VehicleServiceMonitoringSystem.Repositories.Interfaces;

namespace VehicleServiceMonitoringSystem.Controllers
{
    [Authorize]
    public class ServiceJobController : Controller
    {
        private readonly IServiceJobRepository _serviceJobRepository;

        public ServiceJobController(IServiceJobRepository serviceJobRepository)
        {
            _serviceJobRepository = serviceJobRepository;
        }

        // GET: /ServiceJob/Index?keyword=...
        public IActionResult Index(string? keyword)
        {
            var jobs = _serviceJobRepository.Search(keyword);
            ViewData["Keyword"] = keyword;
            return View(jobs);
        }

        // GET: /ServiceJob/Create
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.NextServiceNumber = "AUTO-GENERATED";
            return View(new ServiceJobCreateDto
            {
                CheckInDateTime = DateTime.Now,
                ExpectedReleaseDate = DateTime.Now.AddHours(4)
            });
        }

        // POST: /ServiceJob/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ServiceJobCreateDto dto)
        {
            if (dto.ExpectedReleaseDate <= dto.CheckInDateTime)
                ModelState.AddModelError(nameof(dto.ExpectedReleaseDate), "Expected release must be after check-in.");

            if (!ModelState.IsValid)
            {
                ViewBag.NextServiceNumber = "AUTO-GENERATED";
                return View(dto);
            }

            var job = new ServiceJob
            {
                CustomerName = dto.CustomerName,
                ContactNumber = dto.ContactNumber,
                VehicleMake = dto.VehicleMake,
                VehicleModel = dto.VehicleModel,
                ModelYear = dto.ModelYear,
                PlateNumber = dto.PlateNumber,
                VehicleColor = dto.VehicleColor,
                ServiceType = dto.ServiceType,
                ServiceBay = dto.ServiceBay,
                CheckInDateTime = dto.CheckInDateTime,
                ExpectedReleaseDate = dto.ExpectedReleaseDate,
                Remarks = dto.Remarks,
                Status = ServiceStatus.Waiting
            };

            _serviceJobRepository.Add(job);

            TempData["SuccessMessage"] = $"Vehicle registered successfully as {job.ServiceNumber}.";
            return RedirectToAction(nameof(Index));
        }

        // GET: /ServiceJob/Details/5
        public IActionResult Details(int id)
        {
            var job = _serviceJobRepository.GetById(id);
            if (job is null) return NotFound();

            var dto = MapToDetailsDto(job);
            return View(dto);
        }

        // GET: /ServiceJob/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var job = _serviceJobRepository.GetById(id);
            if (job is null) return NotFound();

            var dto = new ServiceJobEditDto
            {
                Id = job.Id,
                ServiceNumber = job.ServiceNumber,
                CustomerName = job.CustomerName,
                ContactNumber = job.ContactNumber,
                VehicleMake = job.VehicleMake,
                VehicleModel = job.VehicleModel,
                ModelYear = job.ModelYear,
                PlateNumber = job.PlateNumber,
                VehicleColor = job.VehicleColor,
                ServiceType = job.ServiceType,
                ServiceBay = job.ServiceBay,
                CheckInDateTime = job.CheckInDateTime,
                ExpectedReleaseDate = job.ExpectedReleaseDate,
                Status = job.Status,
                Remarks = job.Remarks
            };

            return View(dto);
        }

        // POST: /ServiceJob/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ServiceJobEditDto dto)
        {
            if (id != dto.Id) return BadRequest();

            var existing = _serviceJobRepository.GetById(id);
            if (existing is null) return NotFound();

            if (!ModelState.IsValid)
            {
                dto.ServiceNumber = existing.ServiceNumber;
                return View(dto);
            }

            existing.CustomerName = dto.CustomerName;
            existing.ContactNumber = dto.ContactNumber;
            existing.VehicleMake = dto.VehicleMake;
            existing.VehicleModel = dto.VehicleModel;
            existing.ModelYear = dto.ModelYear;
            existing.PlateNumber = dto.PlateNumber;
            existing.VehicleColor = dto.VehicleColor;
            existing.ServiceType = dto.ServiceType;
            existing.ServiceBay = dto.ServiceBay;
            existing.CheckInDateTime = dto.CheckInDateTime;
            existing.ExpectedReleaseDate = dto.ExpectedReleaseDate;
            existing.Status = dto.Status;
            existing.Remarks = dto.Remarks;

            _serviceJobRepository.Update(existing);

            TempData["SuccessMessage"] = $"Service {existing.ServiceNumber} updated successfully.";
            return RedirectToAction(nameof(Details), new { id });
        }

        // GET: /ServiceJob/Release/5
        [HttpGet]
        public IActionResult Release(int id)
        {
            var job = _serviceJobRepository.GetById(id);
            if (job is null) return NotFound();

            if (job.Status == ServiceStatus.Released)
            {
                TempData["InfoMessage"] = "This vehicle has already been released.";
                return RedirectToAction(nameof(Details), new { id });
            }

            var dto = new ServiceJobReleaseDto
            {
                Id = job.Id,
                ServiceNumber = job.ServiceNumber,
                CustomerName = job.CustomerName,
                VehicleMake = job.VehicleMake,
                VehicleModel = job.VehicleModel,
                PlateNumber = job.PlateNumber,
                ServiceType = job.ServiceType
            };

            return View(dto);
        }

        // POST: /ServiceJob/Release/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ReleaseConfirmed(int id)
        {
            var job = _serviceJobRepository.GetById(id);
            if (job is null) return NotFound();

            job.Status = ServiceStatus.Released;
            job.ActualReleaseDateTime = DateTime.Now;
            _serviceJobRepository.Update(job);

            TempData["SuccessMessage"] = "Vehicle successfully released.";
            return RedirectToAction(nameof(Index));
        }

        private static DetailsDto MapToDetailsDto(ServiceJob job)
        {
            return new DetailsDto
            {
                Id = job.Id,
                ServiceNumber = job.ServiceNumber,
                CustomerName = job.CustomerName,
                ContactNumber = job.ContactNumber,
                VehicleMake = job.VehicleMake,
                VehicleModel = job.VehicleModel,
                ModelYear = job.ModelYear,
                PlateNumber = job.PlateNumber,
                VehicleColor = job.VehicleColor,
                ServiceType = job.ServiceType,
                ServiceBay = job.ServiceBay,
                CheckInDateTime = job.CheckInDateTime,
                ExpectedReleaseDate = job.ExpectedReleaseDate,
                ActualReleaseDateTime = job.ActualReleaseDateTime,
                Status = job.Status,
                Remarks = job.Remarks
            };
        }
    }
}
