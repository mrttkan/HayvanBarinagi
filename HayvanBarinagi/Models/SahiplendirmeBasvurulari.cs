namespace HayvanBarinagi.Models
{
    public class SahiplendirmeBasvurulari
    {
        public int Id { get; set; }
        public int HayvanId { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Telefon { get; set; }
        public string Eposta { get; set; }
        public string Adres { get; set; }
        public string Aciklama { get; set; }
        public string Durum { get; set; } = "Bekliyor";
        public DateTime BasvuruTarihi { get; set; }
    }
}