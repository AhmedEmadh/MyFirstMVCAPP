using MyFirstMVCAPP.Data;
using MyFirstMVCAPP.Repository.Base;

namespace MyFirstMVCAPP.Repository
{
    public class MainRepository<T> : IRepository<T> where T : class
    {
        public MainRepository(AppDbContext context)
        {
            this.Context = context;
        }

        protected AppDbContext Context;

        public T FindByID(int id)
        {
            return Context.Set<T>().Find(id);
        }
    }
}
