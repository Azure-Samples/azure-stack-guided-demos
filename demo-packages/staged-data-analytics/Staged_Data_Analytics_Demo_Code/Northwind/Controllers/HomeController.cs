using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Northwind.Controllers
{
    public class HomeController : Controller
    {
        private readonly Models.InsuranceContext _context;

        public HomeController (Models.InsuranceContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewData["ActivePage"] = "home";
            ViewData["Title"] = "Northwind Insurance";
            HttpContext.Session.SetString("CloudName", GetHostingEnvironment());
            return View();
        }

        public IActionResult ApplyNow()
        {
            ViewData["ActivePage"] = "applynow";
            ViewData["Title"] = "Northwind Insurance - Application";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateApplication(
           [Bind("ZipCode,EmploymentIncome,HouseholdMembers")] Models.Subscriber _subscriber)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    _subscriber.TimeStamp = DateTime.Now;
                    _context.Add(_subscriber);
                    await _context.SaveChangesAsync();
                    TempData["SubscriberId"] = _subscriber.SubscriberID;
                    return RedirectToRoute("showplans");
                }
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                "Try again, and if the problem persists " +
                "see your system administrator." + ex.Message);
            }
            return View(_subscriber);
        }

        public IActionResult Error()
        {
            return View();
        }

        public string GetHostingEnvironment()
        {
            try
            {
                return Environment.GetEnvironmentVariable("WEBSITE_HOSTNAME").Contains("azurewebsites.net") ? "Azure" : "AzureStack";
            }
            catch
            {
                return "Localhost";
            }
        }
    }
}
