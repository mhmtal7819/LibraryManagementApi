using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _context;
        private readonly Lazy<ILibRepository> _libRepository;
        private readonly Lazy<ICategoryRepository> _categoryRepository;

        public RepositoryManager(RepositoryContext context)
        {
            _context = context;
            _libRepository=new Lazy<ILibRepository>(() => new LibRepository(_context));
            _categoryRepository=new Lazy<ICategoryRepository>(() => new CategoryRepository(_context));
        }

        public ILibRepository LibRepository => _libRepository.Value;

        public ICategoryRepository Category => _categoryRepository.Value;

        public async Task SaveAsync()
        {
           await _context.SaveChangesAsync();
        }
    }
}
