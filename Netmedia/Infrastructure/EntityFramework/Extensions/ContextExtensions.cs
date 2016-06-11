using System.Data.Entity;
using System.Linq;

namespace Netmedia.Infrastructure.EntityFramework.Extensions
{
    public static class ContextExtensions
    {
        public static bool IsNotAttached<TContext, TEntity>(this TContext context, TEntity entity)
            where TContext : DbContext
            where TEntity : class
        {
            return context.Set<TEntity>().Local.Any(e => e == entity) == false;
        }

        public static bool IsNotAttachedId<TContext, TEntity>(this TContext context, TEntity entity)
            where TContext : DbContext
            where TEntity : class, IIdentifableEntity
        {
            return context.Set<TEntity>().Local.Any(e => e.Id == entity.Id) == false;
        }

        public static bool IsNotAttachedStringId<TContext, TEntity>(this TContext context, TEntity entity)
            where TContext : DbContext
            where TEntity : class, IIdentifableStringEntity
        {
            return context.Set<TEntity>().Local.Any(e => e.Id == entity.Id) == false;
        }

    }
}