using Data;
using Entity;
using HomeUI.Exception;
using HomeUI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Imazen.WebP;
using Aspose.Imaging;
using Aspose.Imaging.FileFormats.Png;
using Aspose.Imaging.ImageOptions;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using Home.Api.Models;

namespace HomeUI.Controllers
{
    [UserAuthorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IRepository<Expense> reposExpence;
        private IRepository<Household> reposHousehold;
        private IRepository<Spending> reposSpending;

        public HomeController(ILogger<HomeController> logger, IRepository<Expense> reposExpence, IRepository<Household> reposHousehold, IRepository<Spending> reposSpending)
        {
            this.reposExpence = reposExpence;
            this.reposHousehold = reposHousehold;
            this.reposSpending = reposSpending;
            _logger = logger;
        }        
        private  Household household { get { 
            return  HttpContext.Session.GetObjectFromJson<Household>("User");
            } set { } }
        public IActionResult Index()
        {
            try
            {
                string url = @"https://api.stackexchange.com/2.2/answers?order=desc&sort=activity&site=stackoverflow";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:3479/api/Expenses/GetList");
                request.AutomaticDecompression = DecompressionMethods.GZip;


                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())

                    

                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    string aaa = reader.ReadToEnd();

                    var result = JsonConvert.DeserializeObject<ServiceResponse>(aaa);
                }




                var model = new HomeIndexVModel();
                model.Expenses = reposExpence.GetAll().Where(x=>x.HouseholdId==household.Id).ToList();
                model.TodayExpense = model.Expenses.Where(x => x.DateofEntry.Date == DateTime.Now.Date).Sum(x => x.Amount);
                model.WeekedExpense = model.Expenses.Where(x => x.DateofEntry.Date.AddDays(7) > DateTime.Now.Date).Sum(x => x.Amount);
                model.MonthlyExpense = model.Expenses.Where(x => x.DateofEntry.Date.AddDays(30) > DateTime.Now.Date).Sum(x => x.Amount);
                model.Expenses = reposExpence.GetWthLinkedTables("Spending", "Household").Where(x=>x.HouseholdId==household.Id);
                model.userExpenses = new List<UserExpense>();
                var users = model.Expenses.GroupBy(x => x.Household);

                if (users.Count() > 0)
                {
                    foreach (var item in users)
                    {

                        model.userExpenses.Add(new UserExpense
                        {
                            Amount = model.Expenses.Where(x => x.HouseholdId == item.Key.Id).Sum(x => x.Amount),
                            User = item.Key.FirstName + " " + item.Key.LastName
                        });

                    }
                }

                return View(model);

            }
            catch (System.Exception ex)
            {
                ViewBag.ms = ex.Message;
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public IActionResult Detay(FilterModel filter)
        {
            var model = new HomeIndexVModel();
            model.Expenses = reposExpence.GetWthLinkedTables("Spending", "Household");

            if (filter != null)
            {
                model.Expenses = filter.HouseholdID > 0 ? model.Expenses.Where(x => x.HouseholdId == filter.HouseholdID) : model.Expenses;
                model.Expenses = filter.SpendingsID > 0 ? model.Expenses.Where(x => x.SpendingId == filter.SpendingsID) : model.Expenses;
                model.Expenses = filter.BeginDate != null ? model.Expenses.Where(x => x.DateofEntry > filter.BeginDate) : model.Expenses;
                model.Expenses = filter.EndDate != null ? model.Expenses.Where(x => x.DateofEntry < filter.EndDate) : model.Expenses;
                model.Expenses = filter.Amount > 0 ? model.Expenses.Where(x => x.Amount > Convert.ToDouble(filter.Amount)) : model.Expenses;
                model.Expenses = !string.IsNullOrEmpty(filter.Name) ? model.Expenses.Where(x => x.Description.Contains(filter.Name)) : model.Expenses;
            }
            model.Expenses = model.Expenses.ToList();
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Index2()
        {
            return View  ();
        }
        [HttpPost]
        public IActionResult Index2(IFormFile file)
        {

            string normalImagePath = System.IO.Path.Combine(@"C:\Users\Samet\Desktop\restest\" + file.FileName);
            string webPFileName = System.IO.Path.GetFileNameWithoutExtension(@"C:\Users\Samet\Desktop\restest\testtt") + "yeni.webp";


            var webpFileName = @"C:\Users\Samet\Desktop\restest\"+"test.webp";
            using (Bitmap bitmap = new Bitmap(normalImagePath))
            {
                using (var saveImageStream = System.IO.File.Open(webpFileName, FileMode.Create))
                {
                    var encoder = new SimpleEncoder();
                    encoder.Encode(bitmap, saveImageStream,60);
                }
            }

          //  using (Aspose.Imaging.FileFormats.Webp.WebPImage image =
          //(Aspose.Imaging.FileFormats.Webp.WebPImage)Image.Load(normalImagePath))
          //  {
          //      Aspose.Imaging.ImageOptions.PdfOptions options = new PdfOptions();
          //      options.PdfDocumentInfo = new Aspose.Imaging.FileFormats.Pdf.PdfDocumentInfo();

          //      image.Save("Animation.pdf", options);
          //  }

            return View();
        }
    }
}
