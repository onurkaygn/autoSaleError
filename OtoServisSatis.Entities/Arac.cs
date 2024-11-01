using System.ComponentModel.DataAnnotations;

namespace OtoServisSatis.Entities
{
    public class Arac : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Marka Adı"), Required(ErrorMessage = "{0} Boş Bırakılamaz!")]
        public int MarkaId { get; set; }
        [StringLength(50), Required(ErrorMessage = "{0} Boş Bırakılamaz!")]
        public string Renk { get; set; }
        public decimal Fiyati { get; set; }
        [StringLength(50), Required(ErrorMessage = "{0} Boş Bırakılamaz!")]
        public string Modeli { get; set; }
        [StringLength(50), Required(ErrorMessage = "{0} Boş Bırakılamaz!")]
        public string KasaTipi { get; set; }
        public int ModelYili { get; set; }
        [Display(Name = "Satışta Mı?")]
        public bool SatistaMi { get; set; }
        [Required(ErrorMessage = "{0} Boş Bırakılamaz!")]
        public string Notlar { get; set; }

        [StringLength(100)]
        public string? Resim1 { get; set; }

        [StringLength(100)]
        public string? Resim2 { get; set; }

        [StringLength(100)]
        public string? Resim3 { get; set; }

        public virtual Marka? Marka { get; set; } 
    }
}
