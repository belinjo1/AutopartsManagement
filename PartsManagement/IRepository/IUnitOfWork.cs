using PartsManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartsManagement.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Sektori> Sektoret { get; }
        IGenericRepository<Produkti> Produktet { get; }
        IGenericRepository<Furnitori> Furnitoret { get; }
        IGenericRepository<Komenti> Komentet { get; }
        IGenericRepository<Marka> Markat { get; }
        IGenericRepository<Modeli> Modelet { get; }
        IGenericRepository<Porosia> Porosite { get; }
        IGenericRepository<Qyteti> Qytetet { get; }
        IGenericRepository<Shteti> Shtetet { get; }
        Task Save();
    }
}