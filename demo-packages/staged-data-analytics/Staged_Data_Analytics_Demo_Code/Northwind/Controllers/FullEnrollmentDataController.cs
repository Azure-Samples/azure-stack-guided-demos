using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Northwind.Models;

namespace Northwind.Controllers
{
    public class FullEnrollmentDataController : Controller
    {
        private readonly InsuranceContext _context;

        public FullEnrollmentDataController(InsuranceContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            HttpContext.Session.SetString("CloudName", GetHostingEnvironment());
            return View(FullEnrollmentData.GetFullEnrollmentData(_context));
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