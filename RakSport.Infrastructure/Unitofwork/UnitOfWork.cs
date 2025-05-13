using Microsoft.EntityFrameworkCore;
using RakSport.Domain.Interfaces.GenericInterface;
using RakSport.Domain.Interfaces.OtpCodeInterface;
using RakSport.Domain.Interfaces.UnitOfWorkInterface;
using RakSport.Infrastructure.DbContext;
using RakSport.Infrastructure.Repositories.Genericrepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RakSport.Infrastructure.Unitofwork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataBaseContext _context;
        private readonly IOtpCodeRepository _otpCodeRepository;
        private readonly Dictionary<Type, object> _repositories = new();
        public UnitOfWork(DataBaseContext context, IOtpCodeRepository otpCodeRepository)
        {
            _context = context;
            _otpCodeRepository = otpCodeRepository;
        }

        public IOtpCodeRepository OtpCodeRepository => throw new NotImplementedException();

        public IGenericRepository<T> Repository<T>() where T : class
        {
            if (!_repositories.ContainsKey(typeof(T)))
            {
                var repository = new GenericRepository<T>(_context);
                _repositories.Add(typeof(T), repository);
            }
            return (IGenericRepository<T>)_repositories[typeof(T)];
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
