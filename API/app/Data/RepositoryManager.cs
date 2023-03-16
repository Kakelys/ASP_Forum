using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Data.Interfaces;

namespace app.Data
{
    public class RepositoryManager : IRepositoryManager
    {

        private readonly RepositoryContext _context;

        public RepositoryManager(
            RepositoryContext context
        )
        {
            _context = context;
        }
            

        public void BeginTransaction() =>
            _context.Database.BeginTransaction();

        public async Task BeginTransactionAsync() =>
            await _context.Database.BeginTransactionAsync();
        
        public void Commit() =>
            _context.Database.CommitTransaction();

        public async Task CommitAsync() =>
            await _context.Database.CommitTransactionAsync();

        public void Rollback() =>
            _context.Database.RollbackTransaction();

        public async Task RollbackAsync() =>
            await _context.Database.RollbackTransactionAsync();

        public void Save() =>
            _context.SaveChanges();

        public async Task SaveAsync() =>
            await _context.SaveChangesAsync();
    }
}