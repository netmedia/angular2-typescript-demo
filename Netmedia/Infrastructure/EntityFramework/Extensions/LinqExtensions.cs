using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;

namespace Netmedia.Infrastructure.EntityFramework.Extensions
{
    public static class LinqExtensions
    {
        // TODO MARKO
        //public static IQueryable LeftOuterJoin<T1, T2>(this IQueryable<T1> query, IEnumerable<T2> query2, Expression<Func<T1, bool>> left, Expression<Func<T2, bool>> right)
        //{
        //    var q1 = query.GroupJoin(query2, left, right, (a, b) => new { a, groupB = b.DefaultIfEmpty() });
        //    var q2 = q1.SelectMany(x => x.groupB, (a, b) => new {a.a, b});
        //    var q3 = Queryable.SelectMany(query, a => q2.Where(x => (x.a == a).Select(x => x.b), (a, b) => new { a, b });
        //}
    }
}
