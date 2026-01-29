using AutoMapper;
using BLL.DTOs.Worker;
using BLL.Services.Interfaces;
using DAL.Models;
using JobFinder.WebApp.ViewModels.Application;
using JobFinder.WebApp.ViewModels.JobOffer;
using JobFinder.WebApp.ViewModels.Worker;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace JobFinder.WebApp.Controllers
{
    public class WorkerController : BaseController
    {

        private readonly IWorkerService _workerService;
        private readonly IJobApplicationService _jobApplicationService;
        private readonly IJobOfferService _jobOfferService;
        private readonly IMapper _mapper;

        public WorkerController(IWorkerService workerService, IJobApplicationService jobApplicationService, IJobOfferService jobOfferService, IMapper mapper)
        {
            _workerService = workerService;
            _jobApplicationService = jobApplicationService;
            _jobOfferService = jobOfferService;
            _mapper = mapper;
        }

        public async Task <IActionResult> Index()
        {

            int firmID = FirmId ?? 0;

            var response = await _jobOfferService.GetByFirmAsync(firmID);

            var converter = _mapper.Map<List<JobOfferListVM>>(response);

            //if (!response.IsSuccessStatusCode)
            //    return View(new List<JobOfferListVM>());
            return View(converter);
        }


        public async Task<IActionResult> Details(int id)
        {
            //var response = await _client.GetAsync("api/jobapplication/by-firm/" + FirmId+"/applied");


            var jobApplicationApprovedList = await _jobApplicationService.GetByOfferApprovedAsync(id);

            var f1 = _mapper.Map<List<WorkerDetailsVM>>(jobApplicationApprovedList);

            var workerList = await _workerService.GetAllByJobOfferAsync(id);

            var f2 = _mapper.Map<List<WorkerDetailsVM>>(workerList);

            var uniqueFromF1 = f1
                .Where(x => !f2
                .Any(y => y.JobApplicationId == x.JobApplicationId))
                .ToList();

            //if (!response.IsSuccessStatusCode)
            //    return View(new List<JobApplicationUsers>());

            f2.AddRange(uniqueFromF1);

            return View(f2);
        }

        public async Task<IActionResult> StartWork(int id)
        {
            try
            {
                WorkerCreateDto dto = new(){ JobApplicationId = id };
                

                var result = await _workerService.CreateAsync(dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> FinishWork(int id)
        {
            await _workerService.FinishAsync(id);
            return NoContent();
        }

    }
}
