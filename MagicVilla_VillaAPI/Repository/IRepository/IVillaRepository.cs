using MagicVilla_VillaAPI.Models;
using System.Linq.Expressions;

namespace MagicVilla_VillaAPI.Repository.IRepository
{
    public interface IVillaRepository
    {
        Task<IEnumerable<Villa>> GetAll(Expression<Func<Villa, bool>> filter = null);

        Task<Villa> Get(Expression<Func<Villa, bool>> filter = null, bool tracked = true);

        Task Create(Villa enity);

        Task Remove(Villa enity);

        Task Save();
    }
}
