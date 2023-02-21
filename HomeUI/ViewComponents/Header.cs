using Data;
using Entity;
using HomeUI.Exception;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeUI.ViewComponents
{
    public class Header : ViewComponent
    {
        private readonly IRepository<Household> _repos;
        public Header(IRepository<Household> repos)
        {
            _repos = repos;
        }
        public IViewComponentResult Invoke()
        {
            //parametre alıp almadığını kontrol ediyorum    
            //var person = new Household();
            var person = HttpContext.Session.GetObjectFromJson<Household>("User");
            return View(person);
        }
    }
}