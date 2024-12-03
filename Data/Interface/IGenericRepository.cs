namespace Medical.Data.Interface;

public interface IGenericRepository<TEntity> where TEntity : class
{
    public Task<IEnumerable<TEntity>> GetAll();
    public Task<TEntity?> GetById(int id);

    public Task Add(TEntity entity);
    public void Update(TEntity entity);
    public void Delete(TEntity entity);

}
