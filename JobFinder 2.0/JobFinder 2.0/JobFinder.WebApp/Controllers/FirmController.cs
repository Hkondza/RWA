using AutoMapper;
using BLL.Services.Interfaces;
using DAL.Models;
using JobFinder.WebApp.ViewModels.Admin;
using JobFinder.WebApp.ViewModels.Application;
using JobFinder.WebApp.ViewModels.JobOffer;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace JobFinder.WebApp.Controllers
{
    public class FirmController : BaseController
    {

        private readonly IMapper _mapper;
        private readonly IJobOfferService _jobOfferService;
        private readonly IJobApplicationService _jobApplicationService;
        public FirmController(IMapper mapper, IJobOfferService jobOfferService, IJobApplicationService jobApplicationService)
        {
            _mapper = mapper;
            _jobOfferService = jobOfferService;
            _jobApplicationService = jobApplicationService;
        }

        public async Task<IActionResult> Index()
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

            var response = await _jobApplicationService.GetByOfferApprovedAsync(id);
            var converter = _mapper.Map<List<JobApplicationUsers>>(response);

           
            //if (!response.IsSuccessStatusCode)
            //    return View(new List<JobApplicationUsers>());

            return View(converter);
        }


        //pogledaj ovo sutra sta ces za clinet 
        // 
     

        public async Task<IActionResult> Approve(int id)
        {
            var response = _jobApplicationService.ApproveAsync(id);
            return RedirectToAction(nameof(Index));
        }

       
        
        public async Task<IActionResult> Reject(int id)
        {
            var response = _jobApplicationService.RejectAsync(id);
            return RedirectToAction(nameof(Index));
        }



    }
}
