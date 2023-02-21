using Data;
using Entity;
using HomeUI.Exception;
using HomeUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace HomeUI.Controllers
{
    public class ExpenseController : Controller
    {
        private IRepository<Expense> reposExpence = null;
        private IRepository<Spending> reposSpendings = null;
        private IRepository<Household> reposHousehold = null;

        private Household household
        {
            get { return HttpContext.Session.GetObjectFromJson<Household>("User"); }
            set{ }
        }
        public ExpenseController(IRepository<Expense> reposExpence, IRepository<Spending> reposSpendings, IRepository<Household> reposHousehold)
        {         
            this.reposExpence = reposExpence;
            this.reposSpendings = reposSpendings;
            this.reposHousehold = reposHousehold;
        }
        public IActionResult Index(int page =1,int pageSize=1)
        {
            PagedList<Expense> model = new PagedList<Expense>(reposExpence.GetWthLinkedTables("Household", "Spending"),page,pageSize);
            //var model = reposExpence.GetWthLinkedTables("Household","Spending").Where(x=>x.HouseholdId==household.Id).ToPagedList(1,2);
            return View(model);
        }

        [HttpGet]
        public ActionResult _CreateOrEdit(int? id)
        {
            List<SelectListItem> masraf = new List<SelectListItem>();
            masraf.Add(new SelectListItem { Value = "0", Text = "Masraf alanını seç" });
            foreach (var item in reposSpendings.GetAll())
            {
                masraf.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.SpendingArea });
            }

            List<SelectListItem> masrafKisi = new List<SelectListItem>();
            masrafKisi.Add(new SelectListItem { Value = "0", Text = "Masraf yapanı seç" });
            foreach (var item in reposHousehold.GetAll())
            {
                masrafKisi.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.FirstName });
            }
            
            ViewBag.masrafAlan = masraf;
            ViewBag.masrafKisi = masrafKisi;

            if (id != 0)
            {
                Expense model = reposExpence.GetById(id);
                return PartialView(model);
            }            
            else
            {
                Expense model = new Expense();
                return PartialView(model);
            }
        }

        [HttpPost]
        public ActionResult _CreateOrEdit(Expense model)
        {
            model.HouseholdId = household.Id;
            //char sepdec = Convert.ToChar(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
            //model.Amount  = double.Parse(model.Amount.ToString().Replace(',', sepdec).Replace('.', sepdec));
            if (!ModelState.IsValid)
            {
                
                var a = ModelState.Values.SelectMany(d => d.Errors)
                    .Select(e => e.ErrorMessage);
                string errorMes = "";
                foreach (var item in a)
                {
                    errorMes += item.ToString() + " ";
                }
                return Json(new { success = false, data = a });
            }

            if (model.Id != 0)
            {
                var model1 = reposExpence.GetById(model.Id);
                model1.Description = model.Description;
                model1.Amount = model.Amount;
                model1.HouseholdId = model.HouseholdId;
                model1.SpendingId = model.SpendingId;

                reposExpence.Update(model1);
                reposExpence.Save();
                
                return Json(new { success = true, data = "Güncelleme işlemi tamamlandı" });
            }
            
            else
            {
                var model1 = new Expense();
                model1 = model;
                model1.DateofEntry = DateTime.Now;
                reposExpence.Insert(model1);
                reposExpence.Save();
                return Json(new { success = true, data = "Ekleme işlemi başarılı" });

            }
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (id != 0)
            {
                Expense model = reposExpence.GetById(id);
                reposExpence.Delete(id);
                reposExpence.Save();
                return Json(new { success = true, mesaj = "Silme işlemi tamamlandı" });
            }
            
            else
            {
                return Json(new { success = false, mesaj = "Silme işlemi başarısız oldu" });
            }

        }

    }
}
