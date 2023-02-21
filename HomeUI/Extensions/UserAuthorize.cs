using Data;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeUI.Exception
{
    public class UserAuthorize : ActionFilterAttribute, IAuthorizationFilter
    {
        private IRepository<Household> _reposHousehold=new EfCoreRepository<Household>();

        public void OnAuthorization(AuthorizationFilterContext context)
        {

            var a = context.HttpContext.Session.GetObjectFromJson<Household>("User");
            var b = context.HttpContext.Request.Cookies["user"];

            var d= context.HttpContext.Request.Headers["sec-ch-ua"].ToString(); 
            if (a!=null)
            {

                var user = _reposHousehold.GetById(Convert.ToInt32(b));
                if (a.SessionLogin != user.SessionLogin)
                {
                    context.Result = new RedirectResult("~/Account/Login");
                }
            }

            if (a==null && b!=null)
            {
                var user = _reposHousehold.GetById(Convert.ToInt32(b));
                context.HttpContext.Session.SetObjectAsJson("User", user);

                if (user.SessionLogin !=d)
                {
                    context.Result = new RedirectResult("~/Account/Login");
                }

            }

            if (a == null && b == null)
            {
                context.Result = new RedirectResult("~/Account/Login");

            }
        }
    }

}
