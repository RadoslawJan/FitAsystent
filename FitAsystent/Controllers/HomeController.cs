using FitAsystent.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using FitAsystent.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace FitAsystent.Controllers
{
    public class HomeController : Controller
    {
        private readonly FitAsystentContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(FitAsystentContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new HealthData();
            if(User.Identity != null && User.Identity.IsAuthenticated)
            {
                var userId = _userManager.GetUserId(User);
                var lastRecord = _context.HealthRecords.Where(r => r.UserId == userId).OrderByDescending(r => r.DataPomiaru).FirstOrDefault();
                if(lastRecord != null){
                    model.Wzrost = lastRecord.Wzrost;
                    model.Wiek = lastRecord.Wiek;
                    model.Plec = lastRecord.Plec;
                }
            }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Index(HealthData model)
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

            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var userId = _userManager.GetUserId(User);
                var newRecord = new HealthRecord
                {
                    UserId = userId,
                    DataPomiaru = DateTime.Now,
                    Waga = model.Waga,
                    Wzrost = model.Wzrost,
                    Wiek = model.Wiek,
                    Plec = model.Plec,
                    BMI = model.BMI,
                    WynikBMI = model.WynikBMI,
                    ZapotrzebowanieKaloryczne = model.ZapotrzebowanieKaloryczne
                };
                _context.HealthRecords.Add(newRecord);
                await _context.SaveChangesAsync();
                ViewBag.Komunikat = "Wyniki zostały zapisane!";

            }
            else
            {
                ViewBag.Komunikat = "Obliczono! Zaloguj by zapisać";
            }
            return View(model);
        }
        [Authorize]
        [HttpGet]
        public IActionResult Dashboard()
        {
            var userId = _userManager.GetUserId(User);
            var historia = _context.HealthRecords.Where(r => r.UserId == userId).OrderBy(r => r.DataPomiaru).ToList();
            if(historia.Count() == 0)
            {
                ViewBag.Komunikat = "Najpierw wykonaj pomiar!";
                return RedirectToAction("Index");

            }
            return View(historia);
        }

        [Authorize] // wyświetlanie
        [HttpGet]
        public IActionResult Historia() {             
            var userId = _userManager.GetUserId(User);
            var wpisy = _context.HealthRecords.Where(r => r.UserId == userId).OrderByDescending(r => r.DataPomiaru).ToList();
            return View(wpisy);
        }
        [Authorize] // usuwanie 
        [HttpPost]
        public async Task<IActionResult> Usun(int id)
        {
            var userId = _userManager.GetUserId(User);
            var wpis = _context.HealthRecords.FirstOrDefault(r => r.Id == id && r.UserId == userId);
            if (wpis != null)
            {
                _context.HealthRecords.Remove(wpis);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Historia");
        }
        [Authorize] //wyświetlanie do edycji
        [HttpGet]
        public IActionResult Edytuj(int id)
        {
            var userId = _userManager.GetUserId(User);
            var wpis = _context.HealthRecords.FirstOrDefault(r => r.Id == id && r.UserId == userId);
            if (wpis == null) return Content("Znalazlem metode, ale nie znalazlem wpisu w bazie z tym ID!");
            return View(wpis);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edytuj(HealthRecord model)
        {
            var userId = _userManager.GetUserId(User);
            var wpis = _context.HealthRecords.FirstOrDefault(r => r.Id == model.Id && r.UserId == userId);
            if (wpis != null)
            {
                wpis.Waga = model.Waga;
                wpis.Wzrost = model.Wzrost;
                wpis.Wiek = model.Wiek;
                wpis.Plec = model.Plec;
                double wzrostMetry = wpis.Wzrost / 100.0;
                if (wzrostMetry > 0) wpis.BMI = Math.Round(wpis.Waga / (wzrostMetry * wzrostMetry), 2);

                if (wpis.BMI < 18.5) wpis.WynikBMI = "Niedowaga";
                else if (wpis.BMI < 25) wpis.WynikBMI = "Waga prawidłowa";
                else if (wpis.BMI < 30) wpis.WynikBMI = "Nadwaga";
                else wpis.WynikBMI = "Otyłość";
                if (model.Plec == "Mężczyzna")
                {
                    wpis.ZapotrzebowanieKaloryczne = 66.47 + (13.7 * model.Waga) + (5 * model.Wzrost) - (6.76 * model.Wiek);
                }
                else
                {
                    wpis.ZapotrzebowanieKaloryczne = 655.1 + (9.567 * model.Waga) + (1.81 * model.Wzrost) - (4.68 * model.Wiek);
                }
                wpis.ZapotrzebowanieKaloryczne = Math.Round(wpis.ZapotrzebowanieKaloryczne, 0);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Historia");
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
