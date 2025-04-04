using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplicationProject.Models;

//Veri tabanına EF tablo oluşturması için ilgili model sınıflarınızı buraya eklemlisiniz...
namespace WebApplicationProject.Utility
{
    public class AppDbContext : IdentityDbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }

        public DbSet<OyunTuru> OyunTurleri { get; set; }
        public DbSet<Oyun> Oyunlar {  get; set; }
        public DbSet<Kiralama> Kiralamalar { get; set; }

    }
}
