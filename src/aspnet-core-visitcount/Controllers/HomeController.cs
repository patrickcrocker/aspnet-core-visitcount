using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using aspnet_core_visitcount.Managers;

namespace aspnet_core_visitcount.Controllers
{
    public class HomeController : Controller
    {
        CachManager _cache = null;
        public HomeController()
        {
            _cache = new CachManager();
        }
        public IActionResult Index()
        {
            var pageVisitCount = _cache.GetPageVisitCounter("Index");
            ViewData["PageVisitCount"] = pageVisitCount;

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            var pageVisitCount = _cache.GetPageVisitCounter("About");
            ViewData["PageVisitCount"] = pageVisitCount;

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            var pageVisitCount = _cache.GetPageVisitCounter("Contact");
            ViewData["PageVisitCount"] = pageVisitCount;

            return View();
        }

        public IActionResult Error()
        {
            var pageVisitCount = _cache.GetPageVisitCounter("Error");
            ViewData["PageVisitCount"] = pageVisitCount;

            return View();
        }
    }
}
