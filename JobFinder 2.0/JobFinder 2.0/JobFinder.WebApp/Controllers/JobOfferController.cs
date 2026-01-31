using AutoMapper;
using BLL.DTOs.JobOffer;
using BLL.Repositories.Interfaces;
using BLL.Services.Interfaces;
using JobFinder.WebApp.ViewModels.JobOffer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Json;
using System.Text.Json;

namespace JobFinder.WebApp.Controllers
{
    public class JobOfferController : BaseController
    {
        private readonly IJobOfferService _jobOfferService;
        private readonly IJobTypeRepository _jobTypeRepo;
        private readonly ILocationRepository _locationRepo;
        private readonly IFirmService _firmService;
        private readonly IMapper _mapper;

        public JobOfferController(IJobOfferService jobOfferService, IFirmService firmService, IMapper mapper, IJobTypeRepository jobTypeRepo, ILocationRepository locationRepo)
        {
            _jobOfferService = jobOfferService;
            _firmService = firmService;
            _mapper = mapper;
            _jobTypeRepo = jobTypeRepo;
            _locationRepo = locationRepo;
        }


        public async Task<IActionResult> Index(string? search, int page = 1)
        {
            int pageSize = 10;

            var result = await _jobOfferService.GetAllSearchAsync(search, page, pageSize);
            var totalCount = await _jobOfferService.CountAsync(search);

            var vm = _mapper.Map<List<JobOfferListVM>>(result);

            ViewBag.Search = search;
            ViewBag.Page = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            return View(vm);
        }



        public async Task<IActionResult> Details(int id)
        {
            var response = await _jobOfferService.GetByIdAsync(id);

 
            var converter = _mapper.Map<JobOfferDetailsVM>(response);
            //if (!response.IsSuccessStatusCode)
            //    return NotFound();


            ViewBag.CanApply = IsAuthenticated && IsEmployee;

            return View(converter);
        }

    
        public async Task<IActionResult> Create()
        {

            if (FirmId == 0 || !FirmId.HasValue)
            {
                TempData["Error"] = "Ne možeš kreirati oglas jer nemaš dodijeljenu firmu. Postavi firmu u profilu.";
                return RedirectToAction("Index", "Profile");
            }

            var vm = new JobOfferCreateVM();





            var jobTypes = await _jobTypeRepo.GetAllAsync();
            if (jobTypes != null)
            {
                vm.JobTypes = jobTypes
                    .Select(j => new SelectListItem(j.JobName, j.IdjobType.ToString()))
                    .ToList();
            }


            var locations = await _locationRepo.GetAllAsync();
            if (locations != null)
            {
                vm.Locations = locations
                    .Select(l => new SelectListItem(l.LocationName, l.Idlocation.ToString()))
                    .ToList();
            }

            return View(vm);
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(JobOfferCreateVM vm)
        {
           

            if (!vm.JobTypeID.HasValue && string.IsNullOrWhiteSpace(vm.NewJobTypeName))
                ModelState.AddModelError(nameof(vm.NewJobTypeName), "Odaberi tip posla ili upiši novi.");

            if (!vm.LocationID.HasValue && string.IsNullOrWhiteSpace(vm.NewLocationName))
                ModelState.AddModelError(nameof(vm.NewLocationName), "Odaberi lokaciju ili upiši novu.");

            if (!ModelState.IsValid)
            {
                // Ponovno napuni dropdown liste
                return await Create();
            }

            // 🔹 Payload TOČNO kakav API očekuje
            var payload = new JobOfferCreateVM
            {
              Title =  vm.Title,
               Description =  vm.Description,
               Salary = vm.Salary,

                FirmID = FirmId,

                JobTypeID = vm.JobTypeID,
                NewJobTypeName = vm.NewJobTypeName,

                LocationID = vm.LocationID,
                NewLocationName = vm.NewLocationName
            };

            var converter = _mapper.Map<JobOfferCreateDto>(payload);


            var response = await _jobOfferService.CreateAsync(converter);


            //var response = await _client.PostAsJsonAsync("/api/joboffer", payload);

            if (response == null)
            {
                ModelState.AddModelError("", "Greška pri kreiranju oglasa.");
                return await Create();
            }

            return RedirectToAction(nameof(Index));
        }

      
        private class JobTypeLookupDto
        {
            public int IDJobType { get; set; }
            public string JobName { get; set; }
        }

        private class LocationLookupDto
        {
            public int IDLocation { get; set; }
            public string LocationName { get; set; }
        }

     
    }
}
