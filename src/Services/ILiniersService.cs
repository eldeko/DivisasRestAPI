using DivisasRESTAPI.Models.Liniers;

namespace DivisasRESTAPI.Services
{
    public interface ILiniersService
    {
        CategoriaContainer GetLiniersDataAsync(string desde, string hasta);
    }
}
