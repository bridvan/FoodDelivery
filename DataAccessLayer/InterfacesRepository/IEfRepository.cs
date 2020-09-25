namespace DataAccessLayer
{
    public interface IEfRepository
    {
        void Add(object entity);
        void Delete(object entity);
        bool SaveChanges();
        void Update(object entity);
    }
}