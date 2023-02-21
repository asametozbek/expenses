using Data;
using Entity;
using Home.Api.Extensions;
using Home.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Home.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private IRepository<Expense> _reposExpence;

        public ExpensesController(IRepository<Expense> reposExpence)
        {
            _reposExpence = reposExpence;
        }
        // GET: api/<ExpensesController>
        [AllowAnonymous]
        [HttpGet]
        [Route("GetList")]
        public ServiceResponse Index()
        {
            var response = new ServiceResponse();
            response.Data= _reposExpence.GetAll();
            response.IsSuccesful = true;
            return response;
        }

        [AllowAnonymous]
        // POST api/<MembersController>
        [HttpPost("Create")]
        public ServiceResponse Create([FromBody] Expense expense)
        {
            //var token = jwtAuth.Authentication(userCredential.UserName, userCredential.Password);
            var response = new ServiceResponse();
            if (!ModelState.IsValid)
            {
                response.IsSuccesful = false;
                response.Message = "Eklediğiniz alanları eksik veya hatalı girdiniz";

            }
            else
            {
                _reposExpence.Insert(expense);
                response.IsSuccesful = true;
                response.Result = 1;
                response.Message = "Ekleme işlemi başarılı";
            }
            return response;
        }
        [HttpPost]
        [Route("Update/{id}")]
        private ServiceResponse UpdateExpence(int id, [FromBody] Expense model)
        {
            var response = new ServiceResponse();
            if (!ModelState.IsValid)
            {
                response.IsSuccesful = false;
                response.Message = "Eklediğiniz alanları eksik veya hatalı girdiniz";
            }
            else
            {
                var model1 = _reposExpence.GetById(id);
                model1.Description = model.Description;
                model1.Amount = model.Amount;
                model1.HouseholdId = model.HouseholdId;
                model1.SpendingId = model.SpendingId;
                _reposExpence.Update(model1);
                _reposExpence.Save();
                response.Result = 1;
                response.Message = "Ekleme işlemi başarılı";
            }

            return response;
        }

        [HttpPost]
        [Route("Delete/{id}")]

        private ServiceResponse DeleteExpense(int id)
        {
            var response = new ServiceResponse();
            Expense model = _reposExpence.GetById(id);
            _reposExpence.Delete(id);
            _reposExpence.Save();
            response.IsSuccesful = true;
            response.Message = "Silme işlemi tamamlandı";

            return response;
        }

        // GET api/<ExpensesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet("Delete/{id}")]
        public async Task<ServiceResponse> Delete(int id)
        {
            var result = new ServiceResponse();
            var a=_reposExpence.GetById(id);
            if (a!=null)
            {
                await  _reposExpence.DeleteAsync(a);
                //_reposExpence.Save();
                result.IsSuccesful = true;
                result.Message = "silme başarılı";
                return result;
            }
            result.IsSuccesful = false;
            result.Message = "silme işlemi yapılamadı";
            return result;
        }

    }
}
