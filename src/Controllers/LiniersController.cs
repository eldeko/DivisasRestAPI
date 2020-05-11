using DivisasRESTAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;

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

        public IActionResult GetLiniersData(string desde, string hasta)
        {
            var res = _liniersService.GetLiniersDataAsync(desde, hasta);

            return Ok(res);
            
        }
    }
}