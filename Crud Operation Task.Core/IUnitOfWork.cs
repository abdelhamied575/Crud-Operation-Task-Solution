using Crud_Operation_Task.Core.RepositoriesContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud_Operation_Task.Core
{
    public interface IUnitOfWork
    {

        Task<int> CompleteAsync();

        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;



    }
}
