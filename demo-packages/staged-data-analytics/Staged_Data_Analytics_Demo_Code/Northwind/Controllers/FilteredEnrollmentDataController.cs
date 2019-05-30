using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Northwind.Models;
using Northwind.Storage;
using Newtonsoft.Json;

namespace Northwind.Controllers
{
    public class FilteredEnrollmentDataController : Controller
    {
        AzureStorageHelper storageHelper = new AzureStorageHelper(ConfigurationManager.AppSettings["AZ_StorageConnectionString"], ConfigurationManager.AppSettings["AZ_StorageContainerName"]);

        // GET: FilteredEnrollmentData
        public ActionResult Index()
        {
            HttpContext.Session.SetString("CloudName", GetHostingEnvironment());
            var blobArray = storageHelper.GetBlobNamesFromAzureStorage();
            var stringList = blobArray == null ? new List<string>() : blobArray.ToList<string>();
            List<FilteredEnrollmentData> filteredDataList = new List<FilteredEnrollmentData>();

            foreach (string blobName in stringList)
            {
                filteredDataList.Add(JsonConvert.DeserializeObject<FilteredEnrollmentData>(storageHelper.DownloadBlobFromAzureStorage(blobName)));
            }

            return View(filteredDataList);
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