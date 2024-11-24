using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Entities.RequestFeatures;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class LibManager : ILibService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper mapper;
        public LibManager(IRepositoryManager manager,ILoggerService logger,IMapper mapper)
        {
             _manager = manager;
            _logger = logger;
            this.mapper = mapper;
        }
        public async Task<BookDto> CreateOneBookAsync(BookDtoInsertion bookDto)
        {
            var entity = mapper.Map<Libs>(bookDto);
            _manager.LibRepository.CreateOneBook(entity);
            await _manager.SaveAsync();
            return mapper.Map<BookDto>(entity);
        }

        public async Task DeleteOneBookAsync(int id, bool trackChanges)
        {
            var entity = await GetOneBookCheckExist(id, trackChanges);

            _manager.LibRepository.DeleteOneBook(entity);
                await _manager.SaveAsync();
               
            
            
        }

        public async Task<(IEnumerable<BookDto> libs, MetaData metaData)> GetAllBooksAsync(BookParameters bookParameters, bool trackChanges)
        {
            if(!bookParameters.ValidPriceRange)
                throw new PriceOutOfRangeBadRequestException();

            var booksWithMetaData = await _manager
                .LibRepository
                .GetAllBooksAsync(bookParameters, trackChanges);

            var booksDto = mapper.Map<IEnumerable<BookDto>>(booksWithMetaData);
            return (booksDto, booksWithMetaData.MetaData);
        }

        public async Task<IEnumerable<Libs>> GetAllBooksWithDetailsAsync(bool trackChanges)
        {
            return await _manager
                .LibRepository
                .GetAllBooksWithDetailsAsync(trackChanges);
        }

        public async Task<BookDto> GetOneBookAsync(int id, bool trackChanges)
        {
            var entity = await GetOneBookCheckExist(id, trackChanges);
            return mapper.Map<BookDto>(entity);
        }

        public async Task<(BookDtoUpdate bookDtoUpdate, Libs libs)> GetOneBookForPatchAsync(int id, bool trackChanges)
        {
            var book = await GetOneBookCheckExist(id, trackChanges);
            var bookDtoUpdate= mapper.Map<BookDtoUpdate>(book);
            return (bookDtoUpdate, book);
        }

        public async Task SaveChangesForPatchAsync(BookDtoUpdate bookDtoUpdate, Libs libs)
        {
            mapper.Map(bookDtoUpdate,libs);
            await _manager.SaveAsync();
        }

        public async Task UpdateOneBookAsync(int id, BookDtoUpdate bookDto,bool trackChanges)
        {
            var entity=  await GetOneBookCheckExist(id, trackChanges);



                entity =mapper.Map<Libs>(bookDto);

                _manager.LibRepository.UpdateOneBook(entity);
               await _manager.SaveAsync();
                
            
        }
        private async Task<Libs> GetOneBookCheckExist(int id , bool trackChanges)
        {
            var entity = await _manager.LibRepository.GetOneBookAsync(id, true);

            if (entity is null)
            {
                throw new BookNotFoundException(id);
            }
            return entity;
        }
        

        
    }
}
