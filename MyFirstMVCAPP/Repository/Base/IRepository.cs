namespace MyFirstMVCAPP.Repository.Base
{
    public interface IRepository<T> where T : class
    {
        public T FindByID(int id);
    }
}
