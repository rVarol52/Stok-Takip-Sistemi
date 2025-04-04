using WebApplicationProject.Utility;

namespace WebApplicationProject.Models
{
    public class OyunTuruRepository : Repository<OyunTuru>, IOyunTuruRepository
    {
        private AppDbContext _appDbContext;
        public OyunTuruRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Guncelle(OyunTuru oyunTuru)
        {
            _appDbContext.Update(oyunTuru);
        }

        public void Kaydet()
        {
            _appDbContext.SaveChanges();
        }
    }
}
