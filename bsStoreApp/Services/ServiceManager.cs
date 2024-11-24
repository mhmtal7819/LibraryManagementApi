using AutoMapper;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<ILibService> _libService;
        private readonly Lazy<ICategoryService> _categoryService;
        private readonly Lazy<IAuthenticationService> _authService;
        public ServiceManager(IRepositoryManager repositoryManager,ILoggerService logger,IMapper mapper
            ,IConfiguration configuration,UserManager<User> userManager,IBookLinks bookLinks) {

            _libService = new Lazy<ILibService>(() => new LibManager(repositoryManager,logger,mapper));
            _categoryService = new Lazy<ICategoryService>(() => new CategoryManager(repositoryManager));
            _authService = new Lazy<IAuthenticationService>(() =>
       new AuthenticationManager(logger, mapper, userManager, configuration));
        }
        public ILibService libService => _libService.Value;
        public IAuthenticationService AuthenticationService => _authService.Value;

        public ICategoryService CategoryService => _categoryService.Value;

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
