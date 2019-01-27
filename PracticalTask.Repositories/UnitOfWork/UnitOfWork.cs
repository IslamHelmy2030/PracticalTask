using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using PracticalTask.Repositories.Repository;
using Microsoft.EntityFrameworkCore.Storage;

namespace PracticalTask.Repositories.UnitOfWork
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class
    {
        private DbContext _context;
        private IDbContextTransaction _transaction;

        public IRepository<T> Repo { get; }
        //public IAsyncRepository AsyncRepo { get; }

        public UnitOfWork(DbContext context)
        {
            _context = context;
            //AsyncRepo = new AsyncRepository();
            //context.Database.Initialize(force: false);
            //context.Configuration.LazyLoadingEnabled = false;

            //context.Configuration.ProxyCreationEnabled = false;
            Repo = new Repository<T>(context);
        }

        public int SaveChanges()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var error = string.Empty;
                foreach (var eve in e.EntityValidationErrors)
                {
                    error += $"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation errors:";
                    error = eve.ValidationErrors.Aggregate(error, (current, ve) => current + $"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                    error += $"\n";
                }
                throw new Exception(error);
            }
        }

        public void StartTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _transaction.Commit();
            _transaction.Dispose();
        }

        public void Rollback()
        {
            _transaction.Rollback();
            _transaction.Dispose();
        }

        public void SetDbProxyStatus(bool enable)
        {
            _context.Configuration.ProxyCreationEnabled = enable;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (_context == null) return;
            _context.Dispose();
            _context = null;
        }
    }
}
