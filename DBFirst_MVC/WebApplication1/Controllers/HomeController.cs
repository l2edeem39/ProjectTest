using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IDataService dataService, IConfiguration configuration)
        {
            _logger = logger;
            _dataService = dataService;
            _configuration = configuration;
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
            ViewBag.test = "TTT12315468";

            //Exex Stored
            var context = new AdventureWorkContext();
            var aff = context.User.FromSqlRaw("exec HelloWorld @option = 1").ToList();

            var _agencies = _configuration.GetSection("TemplateVmi").Get<string[]>().ToList();
            var localize = _configuration.GetSection("TemplateVmi");
            var template = new List<Template>();
            foreach (var item in localize.GetChildren())
            {
                var temp = new Template();
                temp.key = item.Key;
                temp.value = item.Value;
                template.Add(temp);
            }

            ViewBag.template = template;
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
        public IActionResult IGetData()
        {
            var context = new AdventureWorkContext();
            var a = context.User.ToList();
            var aff = context.User.FromSqlRaw("Select * from [User]").ToList();
            return Ok();
        }
    }
}
