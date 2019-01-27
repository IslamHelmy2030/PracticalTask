using System;
using System.Collections.Generic;
using System.Text;
using PracticalTask.Repositories.Repository;

namespace PracticalTask.Repositories.UnitOfWork
{
    public interface IUnitOfWork<T> : IDisposable where T : class
    {
        IRepository<T> Repo { get; }
        //IAsyncRepository AsyncRepo { get; }
        int SaveChanges();
        void StartTransaction();
        void CommitTransaction();
        void Rollback();
        void SetDbProxyStatus(bool enable);
    }
}
