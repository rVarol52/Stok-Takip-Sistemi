using System.Linq.Expressions;

namespace WebApplicationProject.Models
{
    public interface IRepository<T> where T : class
    {
        // T -> OyunTuru
        IEnumerable<T> GetAll(string? includeProps = null);
        T Get(Expression<Func<T, bool>> filtre, string? includeProps = null);
        void Ekle(T entity);
        void Sil(T entity);
        void SilAralik(IEnumerable<T> entity);
    }
}
