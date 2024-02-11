using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LionHeart.DataAccess;

public class EFUpdateHelper
{
    public static async Task CheckItemsOnDelete<TEntity>(List<TEntity> itemsFromModel, ApplicationDbContext db, Expression<Func<TEntity, bool>> itemsFromDbFilter) where TEntity : class
    {
        var itemsFromDb = await db.Set<TEntity>().AsNoTracking().Where(itemsFromDbFilter).ToListAsync();

        foreach (var item in itemsFromDb)
        {
            if (!itemsFromModel.Contains(item))
            {
                db.Remove(item);
            }
        }
    }
    public static async Task CheckItemsOnAdd<TEntity>(List<TEntity> itemsFromModel, ApplicationDbContext db, Expression<Func<TEntity, bool>> itemsFromDbFilter) where TEntity : class
    {
        var itemsFromDb = await db.Set<TEntity>().AsNoTracking().Where(itemsFromDbFilter).ToListAsync();

        foreach (var item in itemsFromModel)
        {
            if (!itemsFromDb.Contains(item))
            {
                db.Add(item);
            }
        }
    }
}