using System.ComponentModel.DataAnnotations;

namespace OtoServisSatis.Entities
{
    public class Satis : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Araç")]
        public int AracId { get; set; }
        [Display(Name = "Müşteri")]

        public int MusteriId { get; set; }
        public decimal SatisFiyati { get; set; }
        public DateTime SatisTarihi { get; set; }
        [Display(Name = "Araç")]

        public virtual Arac? Arac { get; set; }
        [Display(Name = "Müşteri")]

        public virtual Musteri? Musteri { get; set; }
    }
}
