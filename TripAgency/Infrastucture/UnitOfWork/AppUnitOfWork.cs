using Application.IReositosy;
using Application.IUnitOfWork;
using Domain.Context;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWork
{

    public class AppUnitOfWork : IAppUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly Dictionary<Type, object> _repositories = new();

        public AppUnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IAppRepository<T> Repository<T>() where T : class
        {
            if (_repositories.ContainsKey(typeof(T)))
            {
                return (IAppRepository<T>)_repositories[typeof(T)];
            }

            var repository = new AppRepository<T>(_context);
            _repositories.Add(typeof(T), repository);
            return repository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
