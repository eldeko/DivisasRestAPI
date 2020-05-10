using DivisasRESTAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DivisasRESTAPI.Controllers
{
    [Route("/[controller]")]
    public class LiniersController : Controller
    {
        private readonly ILiniersService _liniersService;

        public LiniersController(ILiniersService liniersService)
        {
            _liniersService = liniersService;
        }

        public IActionResult GetLiniersData()
        {
            var res = _liniersService.GetLiniersDataAsync();

            return Ok(res);
            
        }
    }
}