using PartsManagement.Models;
using PartsManagement.Dtos;
using PartsManagement.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartsManagement.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyContext _context;
        private IGenericRepository<Sektori> _sektoret;
        private IGenericRepository<Produkti> _produktet;
        private IGenericRepository<Furnitori> _furnitoret;
        private IGenericRepository<Komenti> _komentet;
        private IGenericRepository<Marka> _markat;
        private IGenericRepository<Modeli> _modelet;
        private IGenericRepository<Porosia> _porosite;
        private IGenericRepository<Qyteti> _qytetet;
        private IGenericRepository<Shteti> _shtetet;



        public UnitOfWork(MyContext context)
        {
            _context = context;
        }
        public IGenericRepository<Sektori> Sektoret => _sektoret ??= new GenericRepository<Sektori>(_context);
        public IGenericRepository<Produkti> Produktet =>  _produktet ??= new GenericRepository<Produkti>(_context);
        public IGenericRepository<Furnitori> Furnitoret =>  _furnitoret ??= new GenericRepository<Furnitori>(_context);
        public IGenericRepository<Komenti> Komentet =>  _komentet ??= new GenericRepository<Komenti>(_context);
        public IGenericRepository<Marka> Markat =>  _markat ??= new GenericRepository<Marka>(_context);
        public IGenericRepository<Modeli> Modelet =>  _modelet ??= new GenericRepository<Modeli>(_context);
        public IGenericRepository<Porosia> Porosite =>  _porosite ??= new GenericRepository<Porosia>(_context);
        public IGenericRepository<Qyteti> Qytetet =>  _qytetet ??= new GenericRepository<Qyteti>(_context);
        public IGenericRepository<Shteti> Shtetet =>  _shtetet ??= new GenericRepository<Shteti>(_context);
        


        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
