using WebApplicationProject.Utility;

namespace WebApplicationProject.Models
{
    public class OyunRepository : Repository<Oyun>, IOyunRepository
    {
        private AppDbContext _appDbContext;
        public OyunRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Guncelle(Oyun oyun)
        {
            _appDbContext.Update(oyun);
        }

        public void Kaydet()
        {
            _appDbContext.SaveChanges();
        }
    }
}
