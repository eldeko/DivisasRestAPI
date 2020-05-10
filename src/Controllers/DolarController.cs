using DivisasRestApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DivisasRestApi.Controllers
{
    [Route("/[controller]")]
    public class DolarController : Controller
    {
        private readonly IDolarSiService _dolarSiService;

        public DolarController(IDolarSiService dolarSiService)
        {
            _dolarSiService = dolarSiService;
        }

        public IActionResult GetCommentByIdAsync()
        {
            var res = _dolarSiService.GetDolarsiDivisada();

            return Ok(res);
        }
    }
}
