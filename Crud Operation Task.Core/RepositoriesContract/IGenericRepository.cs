using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud_Operation_Task.Core.RepositoriesContract
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> GetAsync(string email);

        Task AddAsync(TEntity entity);

        void Update(TEntity entity);

        void Delete(string id);

    }
}
