using System.Text;
using System.Text.Json;
using AutoMapper;
using BLL.Services.Interfaces;
using JobFinder.WebApp.ViewModels.Admin;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.WebApp.Controllers.Admin
{
   
    public class UserFirmController : BaseController
    {

        private readonly IUserFirmService _userFirmService;
        private readonly IMapper _mapper;


        public UserFirmController(IUserFirmService userFirmService, IMapper mapper)
        {
            _userFirmService = userFirmService;
            _mapper = mapper;
        }

        // 📋 LISTA PENDING ZAHTJEVA
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (!IsAuthenticated || !IsAdmin)
                return Unauthorized();

  
            var response = await _userFirmService.GetPendingAsync();
            var converter = _mapper.Map<List<UserFirmAdminVM>>(response);

            //if (!response.IsSuccessStatusCode)
            //    return View(new List<UserFirmAdminVM>());

            return View(converter);
        }

        // ✅ APPROVE
        [HttpPost]
        public async Task<IActionResult> Approve(int id)
        {
            if (!IsAuthenticated || !IsAdmin)
                return Unauthorized();

            await _userFirmService.ApproveAsync(id);

            return RedirectToAction(nameof(Index));
        }

        // ❌ REJECT
        [HttpPost]
        public async Task<IActionResult> Reject(int id)
        {
            if (!IsAuthenticated || !IsAdmin)
                return Unauthorized();

            await _userFirmService.RejectAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
