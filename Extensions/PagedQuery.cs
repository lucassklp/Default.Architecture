using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Extensions
{
    public static class PagedQuery
    {
        public static IList<T> Page<T>(this IQueryable<T> query, int pageSize, int page)
        {
            return query.Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
    }
}
