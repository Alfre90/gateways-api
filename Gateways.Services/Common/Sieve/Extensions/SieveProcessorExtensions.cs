using AutoMapper;
using Gateways.Services.Common.Models;
using Gateways.Services.Common.Sieve.Pagination;
using Sieve.Models;
using Sieve.Services;

namespace Gateways.Services.Common.Sieve.Extensions
{
    public static class SieveProcessorExtensions
    {
        public static async Task<PagedResult<TDtoOut>> GetPagedAsync<T, TDtoOut>(
            this ISieveProcessor sieveProcessor,
            IQueryable<T> query,
            Func<IQueryable<T>, IQueryable<TDtoOut>>? projectTo = null,
            SieveModel? sieveModel = null,
            IConfigurationProvider? configurationProvider = null
            ) where T : class where TDtoOut : BaseDto
        {
            var result = new PagedResult<TDtoOut>();

            var (pagedQuery, page, pageSize, rowCount, pageCount) = await GetPagedResultAsync(sieveProcessor, query, sieveModel);

            result.CurrentPage = page;
            result.PageSize = pageSize;
            result.RowCount = rowCount;
            result.PageCount = pageCount;

            result.Results = projectTo != null
              ? projectTo(pagedQuery)
          :  pagedQuery.ProjectToList<TDtoOut>(configurationProvider);

            return result;
        }


        public static PagedResult<T> GetPagedResults<T>(
           this ISieveProcessor sieveProcessor,
           SieveModel sieveModel,
           IQueryable<T> source,
           object[]? dataForCustomMethods = null) where T : class
        {
            var result = new PagedResult<T>();
            source = sieveProcessor.Apply(
                sieveModel,
                source,
                dataForCustomMethods,
                applyFiltering: true,
                applyPagination: false,
                applySorting: false);

            // Materialize the query 
            var count = source.Count();

            // Get the query
            source = sieveProcessor.Apply(sieveModel, source, dataForCustomMethods);

            // Materialize the query
            result.Results = source.ToList();

            var page = sieveModel?.Page ?? 1;
            var pageSize = sieveModel?.PageSize ?? 50;

            // Return PagedResults object
            result.CurrentPage = page;
            result.PageSize = pageSize;
            result.RowCount = count;
            result.PageCount = (int)Math.Ceiling((double)count / pageSize);
            return result;
        }




        private static async Task<(
            IQueryable<T> pagedQuery,
            int page,
            int pageSize,
            int rowCount,
            int pageCount
            )> GetPagedResultAsync<T>(
            ISieveProcessor sieveProcessor,
            IQueryable<T> query,
            SieveModel? sieveModel = null
            ) where T : class
        {
            var page = sieveModel?.Page ?? 1;
            var pageSize = sieveModel?.PageSize ?? 50;

            if (sieveModel != null)
            {
                // apply pagination in a later step
                query = sieveProcessor.Apply(sieveModel, query, applyPagination: false);
            }

            var rowCount = await Task.FromResult(query.Count());

            var pageCount = (int)Math.Ceiling((double)rowCount / pageSize);

            var skip = (page - 1) * pageSize;
            var pagedQuery = query.Skip(skip).Take(pageSize);

            return (pagedQuery, page, pageSize, rowCount, pageCount);
        }
    }
}
