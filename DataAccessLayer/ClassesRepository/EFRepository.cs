
namespace DataAccessLayer
{
    public class EfRepository : IEfRepository
    {
        public readonly LearningDbContext _context;

       
        public EfRepository(LearningDbContext context)
        {
            _context = context;
        }

        #region Genaric
        public void Add(object entity)
        {
            _context.Add(entity);
        }

        public void Update(object entity)
        {
            _context.Update(entity);
        }


        public void Delete(object entity)
        {
            _context.Remove(entity);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }
        #endregion


    }
}
    