using FitAsystent.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FitAsystent.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(new HealthData());
        }
        [HttpPost]
        public IActionResult Index(HealthData model)
        {
            double wzrostMetry = model.Wzrost / 100;
            if(wzrostMetry> 0)
            {
                model.BMI = Math.Round(model.Waga / (wzrostMetry * wzrostMetry), 2);

            }
            if (model.BMI < 18.5) model.WynikBMI = "Niedowaga";
            else if (model.BMI < 25) model.WynikBMI = "Waga prawidłowa";
            else if (model.BMI < 30) model.WynikBMI = "Nadwaga";
            else model.WynikBMI = "Otyłość";
            if(model.Plec == "Mężczyzna")
            {
                model.ZapotrzebowanieKaloryczne = 66.47 + (13.7 * model.Waga) + (5 * model.Wzrost) - (6.76 * model.Wiek);

            }
            else
            {
                model.ZapotrzebowanieKaloryczne = 655.1 + (9.567 * model.Waga) + (1.81 * model.Wzrost) - (4.68 * model.Wiek);
            }
            model.ZapotrzebowanieKaloryczne = Math.Round(model.ZapotrzebowanieKaloryczne, 0);
            return View(model);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
