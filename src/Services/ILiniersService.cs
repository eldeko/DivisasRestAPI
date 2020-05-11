using DivisasRESTAPI.Models.Liniers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DivisasRESTAPI.Services
{
    public interface ILiniersService
    {
        CategoriaContainer GetLiniersDataAsync(string desde, string hasta);
    }
}
