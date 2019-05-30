using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Northwind.Models;
using Northwind.Storage;
using System.Configuration;
using Newtonsoft.Json;

namespace Northwind.Controllers
{
    public class PlansController : Controller
    {
        private readonly Models.InsuranceContext _context;
        private static Random random = new Random();
        // GET: Plans
        public PlansController(Models.InsuranceContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ShowPlans()
        {
            ViewData["ActivePage"] = "applynow";
            ViewData["Title"] = "Northwind Insurance - Plans";
            var plans = await _context.InsurancePlans.ToListAsync();
            return View(plans);
        }

        public async Task<IActionResult> PurchasePlan()
        {
            ViewData["ActivePage"] = "applynow";
            ViewData["Title"] = "Northwind Insurance - Purchase Plan";
            ViewData["SubscriberId"] = TempData["SubscriberId"];

            var subscriber = await _context.Subscribers.SingleOrDefaultAsync(m => m.SubscriberID == (int)ViewData["SubscriberId"]);

            return View(subscriber);

        }

        [HttpPost, ActionName("PurchasePlan")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PurchasePlanPost(int? id, IFormCollection collection)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subscriberToUpdate = await _context.Subscribers.SingleOrDefaultAsync(s => s.SubscriberID == Convert.ToInt32(collection["SubscriberId"]));

            if (await TryUpdateModelAsync(
                    subscriberToUpdate,
                    "",
                    s => s.FirstName, s => s.MiddleName, s => s.LastName, s => s.AddressLine1, s => s.AddressLine2, s => s.City, s => s.State, s => s.ZipCode, s => s.County, s => s.PhoneNumber, s => s.EmailAddress, s => s.SocialSecurityNumber, s => s.IsUSCitizen, s => s.IsMilitary, s => s.IsStudent, s => s.IsOnMedicare, s => s.IsOnDisability, s => s.EmploymentIncome, s => s.InvestmentIncome, s => s.AlimonyChildSupport))
            {
                try
                {
                    var _confirmation = RandomString(10);
                    var _enrollment = new Models.Enrollment { InsurancePlanID = (int)id, PlanYear = DateTime.Now.Year, SubscriberID = Convert.ToInt32(collection["SubscriberId"]), ConfirmationCode = _confirmation, TimeStamp = DateTime.Now };
                    TempData["confirmation"] = _confirmation;
                    _context.Add(_enrollment);
                    await _context.SaveChangesAsync();

                    var filteredData = FullEnrollmentData.GetFullEnrollmentData(_context, _enrollment.EnrollmentID);

                    AzureStorageHelper helper = new AzureStorageHelper(ConfigurationManager.AppSettings["AzS_StorageConnectionString"], ConfigurationManager.AppSettings["AzS_StorageContainerName"]);

                    helper.UploadDataToAzureStorage(
                        $"{filteredData.EnrollmentID}-{filteredData.SubscriberID}-{filteredData.HouseholdMemberID}.json",
                        JsonConvert.SerializeObject(filteredData, Formatting.Indented));


                    return RedirectToAction("Confirm");
                }
                catch (DbUpdateException ex)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator." + ex.Message);
                }
            }

            return View(subscriberToUpdate);
        }

        public IActionResult Confirm()
        {
            ViewData["ActivePage"] = "applynow";
            ViewData["Title"] = "Northwind Insurance - Confirmation";
            return View();
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}