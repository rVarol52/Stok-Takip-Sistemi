using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApplicationProject.Utility;

namespace WebApplicationProject.Models
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private readonly AppDbContext _appDbContext;
        internal DbSet<T> dbSet;

        public Repository(AppDbContext appDbContext) 
        {
            _appDbContext = appDbContext;
            this.dbSet = _appDbContext.Set<T>();
            _appDbContext.Oyunlar.Include(k => k.OyunTuru).Include(k => k.OyunTuruId);
        }

        public void Ekle(T entity)
        {
            dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filtre, string? includeProps = null) //bir veri getirir
        {
            IQueryable<T> sorgu = dbSet.Where(filtre);
            if (!string.IsNullOrEmpty(includeProps))
            {
                foreach (var includeProp in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    sorgu = sorgu.Include(includeProp);

                }
            }

            return sorgu.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(string? includeProps= null ) //bütün verileri getirir
        {
            IQueryable<T> sorgu = dbSet;
            if (!string.IsNullOrEmpty(includeProps))
            {
                foreach (var includeProp in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    sorgu = sorgu.Include(includeProp);

                }
            }
                return sorgu.ToList();
        }

        public void Sil(T entity)
        {
            dbSet.Remove(entity);
        }

        public void SilAralik(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }
    }
}
