using AutoMapper;
using BLL.DTOs.JobApplication;
using BLL.Services.Interfaces;
using DAL.Models;
using JobFinder.WebApp.ViewModels.Application;
using JobFinder.WebApp.ViewModels.JobOffer;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace JobFinder.WebApp.Controllers
{
    public class ApplicationController : BaseController
    {

        private readonly IJobApplicationService _jobApplicationService;
        private readonly IMapper _mapper;

        public ApplicationController(IJobApplicationService jobApplicationService, IMapper mapper)
        {
            _jobApplicationService = jobApplicationService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {

            int userId = int.Parse(UserId);

            if (userId == 0)
                return View(new List<JobApplicationListVM>());


            var list = await _jobApplicationService.GetByUserAsync(userId);
            var converter = _mapper.Map<JobApplicationListVM>(list);

            return View(converter);
        }


        
        public async Task<IActionResult> Apply(JobApplicationVM vm)
        {
            // 1. mora biti login
            if (!IsAuthenticated)
                return RedirectToAction("Login", "Account");

            // 2. mora biti Employee
            if (!IsEmployee)
                return Forbid();

            if (!ModelState.IsValid)
                return RedirectToAction("Details", "JobOffer", new { id = vm.JobOfferID });


            try
            {
                vm.UserID = int.Parse(UserId);
            }
            catch (Exception)
            {
                throw new Exception("Problem u cookieu");
               
            }


            var converter = _mapper.Map<JobApplicationCreateDto>(vm);
            var response = await _jobApplicationService.ApplyAsync(converter);


            if (response.UserID == 0)
            {
                TempData["Error"] = "Prijava na posao nije uspjela.";
                return RedirectToAction("Details", "JobOffer", new { id = vm.JobOfferID });
            }

            TempData["Success"] = "Uspješno ste se prijavili na posao.";
            return RedirectToAction("");
        }


        public async Task<IActionResult> Details(int id)
        {

            var response = await _jobApplicationService.GetByOfferAsync(id);

            var converter = _mapper.Map<List<JobApplicationDetailsVM>>(response);


            //if (response.)
            //    return NotFound();


            //Samo sam moga iz baze dokvatit jedan objakt umjest liste
            var application = converter.FirstOrDefault();

            if (application == null)
                return NotFound();

            return View(application);

        }

    }


}
