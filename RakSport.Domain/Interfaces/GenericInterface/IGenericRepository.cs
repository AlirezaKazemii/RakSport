using RakSport.Domain.Models.RefreshToken;
using RakSport.Domain.Models.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RakSport.Domain.Interfaces.GenericInterface
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid Id);

        Task<List<T>> GetAllAsync();

        Task AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);
        
    }
}
