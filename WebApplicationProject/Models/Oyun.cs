using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplicationProject.Utility;

namespace WebApplicationProject.Models
{
    public class Oyun
    {
        
        [Key]
        public int Id { get; set; }

        [Required]
        public string OyunAdi { get; set; }

        public string Tanim { get; set;}

        [Required]
        public string Yayıncı { get; set; }

        [Required]
        [Range(10, 5000)]
        public double Fiyat {  get; set; }

        [ValidateNever]
        public int OyunTuruId {  get; set; }
        [ForeignKey("OyunTuruId")]
        [ValidateNever]
        public OyunTuru OyunTuru { get; set; }

    }
}

