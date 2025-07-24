using Microsoft.EntityFrameworkCore;
using MyFirstMVCAPP.Data;
using MyFirstMVCAPP.Repository.Base;
using System.Linq.Expressions;

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

        public IEnumerable<T> FindAll(params string[] agers)
        {
            IQueryable<T> query = Context.Set<T>();

            if (agers.Length > 0)
            {
                foreach (var ager in agers)
                {
                    query = query.Include(ager);
                }
            }

            return query.ToList();
        }

        public async Task<IEnumerable<T>> FindAllAsync(params string[] agers)
        {
            IQueryable<T> query = Context.Set<T>();
            if (agers.Length > 0)
            {
                foreach (var ager in agers)
                {
                    query = query.Include(ager);
                }
            }
            return await query.ToListAsync();
        }

        public T SelectOne(Expression<Func<T, bool>> match)
        {
            return Context.Set<T>().SingleOrDefault(match);
        }

        public async Task<T> SelectOneAsync(Expression<Func<T, bool>> match)
        {
            return await Context.Set<T>().SingleOrDefaultAsync(match);
        }
    }
}
