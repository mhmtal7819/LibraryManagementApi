using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IRepositoryManager
    {
        ILibRepository LibRepository { get; } //Librepoistory classını döndürüyoruz 
        ICategoryRepository Category{ get; } //categoryRepo. classını döndürüyoruz 
        Task SaveAsync(); //kaydetme işlemleri
    }
}
