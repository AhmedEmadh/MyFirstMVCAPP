using MyFirstMVCAPP.Data;
using MyFirstMVCAPP.Models;
using MyFirstMVCAPP.Repository.Base;

namespace MyFirstMVCAPP.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Categories = new MainRepository<Category>(_context);
            Items = new MainRepository<Item>(_context);
            Employees = new MainRepository<Employee>(_context);
        }
        private readonly AppDbContext _context;
        public IRepository<Category> Categories { get; private set; }

        public IRepository<Item> Items { get; private set; }

        public IRepository<Employee> Employees { get; private set; }

        public int ComitChanges()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }
    }
}
