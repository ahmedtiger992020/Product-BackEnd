namespace Sample.Core.Interfaces.Repository
{
    public interface IUpdateRepository<T> where T : class 
    {
        #region Update
         void Update(T entity);
        #endregion
    }
}
