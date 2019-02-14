using PracticalTask.Repositories.Repository;
using System;
using System.Threading.Tasks;

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
