using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KutuphaneKitapKiralama.Models
{
    public class KitapTuru
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Kitap Turu Bos Birakilamaz...")]
        [DisplayName("Kitap Turu")]
        [MaxLength(25)]
        public string Ad { get; set; }
    }
}
