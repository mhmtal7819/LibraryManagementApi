using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public static class LibRepositoryExtension
    {
        public static IQueryable<Libs> FilterBooks(this IQueryable<Libs> books, uint minPrice, uint maxPrice) =>
      books.Where(book =>
          (book.Price >= minPrice) &&
          (book.Price <= maxPrice));

        public static IQueryable<Libs> Search(this IQueryable<Libs> books, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return books;

            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return books
                .Where(b => b.Title
                .ToLower()
                .Contains(lowerCaseTerm));
        }
        public static IQueryable<Libs> Sort(this IQueryable<Libs> books, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return books.OrderBy(b => b.Id);

           var orderQuery= OrderQueryBuilder.CreateOrderQuery<Libs>(orderByQueryString);

            if(orderQuery is null)
                return books.OrderBy(b=>b.Id);  

            return books.OrderBy(orderQuery);
          
        }

    }


}
