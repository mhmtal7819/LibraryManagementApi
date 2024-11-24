using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class LibRepository : RepositoryBase<Libs>, ILibRepository
    {
        public LibRepository(RepositoryContext context) : base(context)
        {
        }

        public void CreateOneBook(Libs lib) => Create(lib);
        

        public void DeleteOneBook(Libs lib) => Delete(lib);


        public async Task<PagedList<Libs>> GetAllBooksAsync(BookParameters bookParameters, bool trackChanges)
        {
            var books = await FindAll(trackChanges)
                .FilterBooks(bookParameters.MinPrice, bookParameters.MaxPrice)
                .Search(bookParameters.SearchTerm)
                .OrderBy(b=>b.Id)
                .ToListAsync();


            return PagedList<Libs>
                .ToPagedList(books,
                    bookParameters.PageNumber,
                    bookParameters.PageSize);
        }

        public async Task<IEnumerable<Libs>> GetAllBooksWithDetailsAsync(bool trackChanges)
        {
            return await _context
                .Libs
                .Include(b => b.Category)
                .OrderBy(b => b.Id)
                .ToListAsync();
        }

        public async Task<Libs> GetOneBookAsync(int id, bool trackChanges) => 
            await FindByCondition(b => b.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();
        

        public void UpdateOneBook(Libs lib) => Update(lib);
        
    }
}
