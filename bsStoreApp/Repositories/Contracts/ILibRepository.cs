using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface ILibRepository : IRepositoryBase<Libs>
    {
        Task<PagedList<Libs>> GetAllBooksAsync(BookParameters bookParameters,bool trackChanges);
        Task<Libs> GetOneBookAsync(int id,bool trackChanges);
        void CreateOneBook(Libs libs);
        void UpdateOneBook(Libs libs);
        void DeleteOneBook(Libs libs);
        Task<IEnumerable<Libs>> GetAllBooksWithDetailsAsync(bool trackChanges);
    }
}
