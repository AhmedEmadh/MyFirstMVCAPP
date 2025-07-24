using Microsoft.EntityFrameworkCore;
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
        public IEnumerable<T> FindAll()
        {
            return Context.Set<T>().ToList();
        }

        public async Task<T> FindByIDAsync(int id)
        {
            return await Context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> FindAllAsync()
        {
            return await Context.Set<T>().ToListAsync();
        }
    }
}
