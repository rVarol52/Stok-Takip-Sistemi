using WebApplicationProject.Utility;

namespace WebApplicationProject.Models
{
    public class KiralamaRepository : Repository<Kiralama>, IKiralamaRepository
    {
        private AppDbContext _appDbContext;
        public KiralamaRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Guncelle(Kiralama kiralama)
        {
            _appDbContext.Update(kiralama);
        }

        public void Kaydet()
        {
            _appDbContext.SaveChanges();
        }
    }
}
