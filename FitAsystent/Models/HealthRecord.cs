using Microsoft.AspNetCore.Identity;

namespace FitAsystent.Models
{
    public class HealthRecord
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public IdentityUser User { get; set; }

        public DateTime DataPomiaru { get; set; } = DateTime.Now;

        public double Waga { get; set; }
        public double Wzrost { get; set; }
        public int Wiek { get; set; }
        public string Plec {  get; set; }

        public double BMI { get; set; }
        public string WynikBMI { get; set; }
        public double ZapotrzebowanieKaloryczne { get; set; }
    }
}
