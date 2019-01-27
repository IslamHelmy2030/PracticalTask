using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PracticalTask.Repositories.Repository;

namespace PracticalTask.Repositories.UnitOfWork
{
    public interface IUnitOfWork<T> : IDisposable where T : class
    {
        IRepository<T> Repo { get; }
        Task<int> SaveChanges();
        void StartTransaction();
        void CommitTransaction();
        void Rollback();
    }
}
