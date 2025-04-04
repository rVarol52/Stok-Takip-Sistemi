namespace WebApplicationProject.Models
{
    public interface IOyunRepository : IRepository<Oyun>
    {
        void Guncelle(Oyun oyun);
        void Kaydet();

    }
}
