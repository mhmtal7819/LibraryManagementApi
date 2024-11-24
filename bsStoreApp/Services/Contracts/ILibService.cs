using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface ILibService
    {
        Task<(IEnumerable<BookDto> libs,MetaData metaData)> GetAllBooksAsync(BookParameters bookParameters,bool trackChanges);
        Task<BookDto> GetOneBookAsync(int id, bool trackChanges); //nesne döndürmesi için sınıf değişkeni
        Task<BookDto> CreateOneBookAsync(BookDtoInsertion book);
        Task UpdateOneBookAsync(int id,BookDtoUpdate bookDto,bool trackChanges);   
        Task DeleteOneBookAsync(int id,bool trackChanges);
        Task<(BookDtoUpdate bookDtoUpdate,Libs libs)> GetOneBookForPatchAsync(int id,bool trackChanges);
        Task SaveChangesForPatchAsync(BookDtoUpdate bookDtoUpdate,Libs libs);
        Task<IEnumerable<Libs>> GetAllBooksWithDetailsAsync(bool trackChanges);

    }
}
