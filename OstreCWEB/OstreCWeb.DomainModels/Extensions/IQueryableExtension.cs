using Microsoft.EntityFrameworkCore;
using OstreCWeb.DomainModels.Collections;

namespace OstreCWEB.Services.Extensions
{
    public static class IQueryableExtension
    {
        public static async Task<PagedList<T>> GetPaginatedListAsync<T>(this IQueryable<T> queryable, int page,
            int pageSize) where T : class
        {
            var result = new PagedList<T>();
            result.CurrentPage = page;
            result.PageSize = pageSize;
            result.ItemCount = queryable.Count();

            var pageCount = (double)result.ItemCount / pageSize;

            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;

            result.Items = await queryable.Skip(skip).Take(pageSize).ToListAsync();

            return result;
        }

        public static PagedList<T> GetPagedList<T>(this IQueryable<T> queryable, int page, int pageSize)
            where T : class

        {
            var result = new PagedList<T>();
            result.CurrentPage = page;
            result.PageSize = pageSize;
            result.ItemCount = queryable.Count();

            var pageCount = (double)result.ItemCount / pageSize;

            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;

            result.Items = queryable.Skip(skip).Take(pageSize).ToList();

            return result;
        }
    }
}
