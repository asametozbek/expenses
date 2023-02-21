using Data;
using Entity;
using HomeUI.Exception;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;


namespace HomeUI.Controllers
{
    [UserAuthorize]

    public class SpendingsController : Controller
    {
        private IRepository<Expense> reposExpence = null;
        private IRepository<Spending> reposSpendings = null;
        private IRepository<Household> reposHousehold = null;

        public SpendingsController(IRepository<Expense> reposExpence, IRepository<Spending> reposSpendings, IRepository<Household> reposHousehold)
        {
            this.reposExpence = reposExpence;
            this.reposSpendings = reposSpendings;
            this.reposHousehold = reposHousehold;
        }
        public IActionResult Index()
        {
            var model = reposSpendings.GetAll();//.GetWthLinkedTables("Household", "Spending");
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
                Spending model = reposSpendings.GetById(id);
                return PartialView(model);
            }
            else
            {
                Spending model = new Spending();
                return PartialView(model);
            }
        }

        [HttpPost]
        public ActionResult _CreateOrEdit(Spending model)
        {
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
                var model1 = reposSpendings.GetById(model.Id);
                model1.SpendingArea = model.SpendingArea;
                model1.ModifiedDate = DateTime.Now;

                reposSpendings.Update(model1);
                reposSpendings.Save();

                return Json(new { success = true, data = "Güncelleme işlemi tamamlandı" });
            }

            else
            {
                var model1 = new Spending();
                model1 = model;
                model1.CreatedDate = DateTime.Now;
                reposSpendings.Insert(model1);
                reposSpendings.Save();
                return Json(new { success = true, data = "Ekleme işlemi başarılı" });

            }
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (id != 0)
            {
                Spending model = reposSpendings.GetById(id);
                reposSpendings.Delete(id);
                reposSpendings.Save();
                return Json(new { success = true, mesaj = "Silme işlemi tamamlandı" });
            }

            else
            {
                return Json(new { success = false, mesaj = "Silme işlemi başarısız oldu" });
            }

        }
    }
}
