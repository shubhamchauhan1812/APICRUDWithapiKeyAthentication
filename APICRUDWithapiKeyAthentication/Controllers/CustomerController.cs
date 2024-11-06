using APICRUDWithapiKeyAthentication.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace APICRUDWithapiKeyAthentication.Controllers
{
    [ApiController]
    [Route("api/Customer")]
    public class CustomerController : ControllerBase
    {
        private readonly ApplicationContext context;
        private readonly ApiKeyService _apiKeyService;
        public CustomerController(ApplicationContext _context, ApiKeyService apiKeyService)
        {
            context = _context;
            _apiKeyService = apiKeyService;
        }
        [EnableCors("AllowOrigin")]
        [HttpPost("GenerateApiKey")]
        public IActionResult GenerateApiKey([FromBody] string userId)
        {
            var apiKey = _apiKeyService.GenerateApiKey(userId);
            return Ok(new { ApiKey = apiKey });
        }

        [EnableCors("AllowOrigin")]
        [HttpGet]
        [Route("GetAllCustomers")]
        public IActionResult GetAllCustomers()
        {
            var data = context.Customers.ToList();
            if (data.Count() == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(data);
            }


        }

        
        public string GetData()
        {
            return "Good day";
        }
        [HttpGet("GetCustomerById/{id}")]
        public IActionResult GetCustomerById(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            else
            {
                var data = context.Customers.Where(e => e.Id == id).SingleOrDefault();
                if (data == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(data);
                }
            }


        }


        [HttpPost]
        [Route("AddCustomer")]
       

        //[ActionName("StudentAdd")]
        public IActionResult AddCustomer([FromBody] Customer model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                context.Customers.Add(model);
                context.SaveChanges();
                return Ok();

            }

        }
    }
}
