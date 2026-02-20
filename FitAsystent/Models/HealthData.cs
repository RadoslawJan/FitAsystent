namespace FitAsystent.Models
{
    public class HealthData
    {
        public double Waga { get; set; }
        public double Wzrost { get; set; }
        public int Wiek { get; set; }
        public string Plec { get; set; }
        
        
        public double BMI { get; set; }
        public string? WynikBMI { get; set; }
        public double ZapotrzebowanieKaloryczne {  get; set; }


    }
}
