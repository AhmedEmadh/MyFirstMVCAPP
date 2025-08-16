using MyFirstMVCAPP.Models;

namespace MyFirstMVCAPP.Repository.Base
{
    public interface IUnitOfWork: IDisposable
    {
        IRepository<Category> Categories { get; }
        IRepository<Item> Items { get; }
        IEmpRepo Employees { get; }
        int ComitChanges();
    }
}
