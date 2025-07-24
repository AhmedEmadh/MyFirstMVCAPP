using System.Linq.Expressions;

namespace MyFirstMVCAPP.Repository.Base
{
    public interface IRepository<T> where T : class
    {
        public T FindByID(int id);
        IEnumerable<T> FindAll(params string[] agers);
        public IEnumerable<T> FindAll();

        public Task<T> FindByIDAsync(int id);
        public Task<IEnumerable<T>> FindAllAsync();
        Task<IEnumerable<T>> FindAllAsync(params string[] agers);
        T SelectOne(Expression<Func<T, bool>> match);

        Task<T> SelectOneAsync(Expression<Func<T, bool>> match);


    }
}
