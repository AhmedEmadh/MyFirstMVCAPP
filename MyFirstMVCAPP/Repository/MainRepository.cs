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

        public void Add(T entity)
        {
            Context.Set<T>().Add(entity);
            Context.SaveChanges();
        }

        public void AddAsync(T entity)
        {
            Context.Set<T>().Add(entity);
            Context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            Context.Set<T>().Update(entity);
            Context.SaveChanges();
        }

        public Task UpdateAsync(T entity)
        {
            Context.Set<T>().Update(entity);
            return Context.SaveChangesAsync();
        }

        public void Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
            Context.SaveChanges();
        }

        public Task DeleteAsync(T entity)
        {
            Context.Set<T>().Remove(entity);
            return Context.SaveChangesAsync();
        }

        public void AddList(IEnumerable<T> entities)
        {
            Context.Set<T>().AddRange(entities);
            Context.SaveChanges();
        }

        public Task AddListAsync(IEnumerable<T> entities)
        {
            Context.Set<T>().AddRange(entities);
            return Context.SaveChangesAsync();
        }

        public void UpdateList(IEnumerable<T> entities)
        {
            Context.Set<T>().UpdateRange(entities);
            Context.SaveChanges();
        }

        public Task UpdateListAsync(IEnumerable<T> entities)
        {
            Context.Set<T>().UpdateRange(entities);
            return Context.SaveChangesAsync();
        }

        public void DeleteList(IEnumerable<T> entities)
        {
            Context.Set<T>().RemoveRange(entities);
            Context.SaveChanges();
        }

        public Task DeleteListAsync(IEnumerable<T> entities)
        {
            Context.Set<T>().RemoveRange(entities);
            return Context.SaveChangesAsync();
        }
    }
}
