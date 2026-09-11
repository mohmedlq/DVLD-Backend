using DVLD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD.Application.Interfaces
{
    public interface ICrud<T>
    {
        Task<T?> GetByIdAsync(int personId);
        Task<List<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task <bool> UpdateAsync(T Entity);
        Task<bool> DeleteAsync(int Id);


    }
}
