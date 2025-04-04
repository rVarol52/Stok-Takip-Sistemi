using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationProject.Models
{
    public class Kiralama
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string MusteriId { get; set; }

        [ValidateNever]
        public int OyunId { get; set; }
        [ForeignKey("OyunId")]

        [ValidateNever]
        public Oyun Oyun { get; set; }

        public DateTime KiraBaslangic { get; set; }
        public DateTime KiraBitis { get; set;}
    }
}
