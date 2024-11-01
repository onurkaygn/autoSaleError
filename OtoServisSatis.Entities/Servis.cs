using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoServisSatis.Entities
{
    public class Servis : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Servise Geliş Tarihi")]

        public DateTime ServiseGelisTarihi { get; set; }
        [Display(Name = "Aracın Sorunu")]

        public string AracSorunu { get; set; }
        [Display(Name = "Servis Ücreti"), Required(ErrorMessage = "{0} Boş Bırakılamaz!")]

        public decimal ServisUcreti { get; set; }
        [Display(Name = "Servisten Çıkış Tarihi")]
        
        public DateTime ServistenCikisTarihi { get; set; }
        [Display(Name = "Yapılan İşlemler")]

        public string? YapilanIslemler { get; set; }
        [Display(Name = "Araç Garanti Kapsamında Mı?")]

        public bool GarantiKapsamindaMi { get; set; }
        [StringLength(15)]
        [Display(Name = "Araç Plakası")]

        public string AracPlaka { get; set; }
        [StringLength(50), Required(ErrorMessage = "{0} Boş Bırakılamaz!")]
        public string Marka { get; set; }
        [StringLength(50), Required(ErrorMessage = "{0} Boş Bırakılamaz!")]
        public string? Model { get; set; }
        [StringLength(50)]
        [Display(Name = "Kasa Tipi")]

        public string? KasaTipi { get; set; }
        [StringLength(50)]
        [Display(Name = "Şase No")]

        public string? SaseNo { get; set; }
        [Required(ErrorMessage = "{0} Boş Bırakılamaz!")]
        public string Notlar { get; set; }
    }
}
