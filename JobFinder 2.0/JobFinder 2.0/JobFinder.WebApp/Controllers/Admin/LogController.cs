using JobFinder.WebApp.ViewModels.JobOffer;
using Microsoft.AspNetCore.Mvc;
using BLL.Services.Interfaces;
using AutoMapper;
using JobFinder.WebApp.ViewModels.Admin;

namespace JobFinder.WebApp.Controllers.Admin
{
    public class LogController : Controller
    {

        private readonly ILogService _logService;
        private readonly IMapper _mapper;

        public LogController(ILogService logService, IMapper mapper)
        {
            _logService = logService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string? search, int page = 1)
        {
            int pageSize = 10;

            var result = await _logService.GetAllSearchAsync(search, page, pageSize);
            var totalCount = await _logService.CountAsync(search);

            var vm = _mapper.Map<List<LogVM>>(result);

            ViewBag.Search = search;
            ViewBag.Page = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            return View(vm);
        }
    }
}
