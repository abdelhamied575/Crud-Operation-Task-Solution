using Crud_Operation_Task.Core;
using Crud_Operation_Task.Core.RepositoriesContract;
using Crud_Operation_Task.Repository.Context;
using Crud_Operation_Task.Repository.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud_Operation_Task.Repository
{
    public class UnitOfWork : IUnitOfWork
    {


        private readonly AppDbContext _context;
        private Hashtable _repositories;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            _repositories = new Hashtable();
        }


        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();


        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repository = new GenericRepository<TEntity>(_context);

                _repositories.Add(type, repository);

            }

            return _repositories[type] as IGenericRepository<TEntity>;

        }
    }
}
