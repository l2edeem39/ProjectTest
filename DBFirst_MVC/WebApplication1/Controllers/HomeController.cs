using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Entities;
using WebApplication1.Models;
using WebApplication1.Service;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDataService _dataService;

        public HomeController(ILogger<HomeController> logger, IDataService dataService)
        {
            _logger = logger;
            _dataService = dataService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Privacy()
        {
            using (var db = new AdventureWorkContext())
            {
                var a = db.User.ToList();
                
                ViewData["user"] = a;
                ViewBag.Message = a;
            }
            var operationId = _dataService.GetOperationID().ToString();
            var b = _dataService.GetData();
            ViewBag.DataLists = b;
            ViewBag.operationID = operationId;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

		[Route("Insert")]
		[HttpGet]
		public IActionResult Index12()
		{
            //var a = new List<string>();
            using (var db = new AdventureWorkContext())
            {
                var a = db.Student.Select(x => x.Name).ToArray();
                return Ok(a);
            }
            
        }
	}
}
