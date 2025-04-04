using System.ComponentModel.DataAnnotations;

namespace WebApplicationProject.Models
{
    public class OyunTuru
    {
        [Key] //primary key
        public int Id { get; set; }

        [Required(ErrorMessage ="Oyun Türü Adı Boş Bırakılamaz!")] // not null
        [MaxLength(25)]
        public string Ad {  get; set; }
    }
}
