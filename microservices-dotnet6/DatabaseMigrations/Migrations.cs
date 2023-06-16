using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Shopping.DatabaseMigrations
{
    public static class Migrations
    {
        public static void ApplyMigrations(this DbContext _context)
        {
            if (_context.Database.CanConnect())
            {
                var hasTables = _context.Database.GetService<IRelationalDatabaseCreator>().HasTables();

                if (!hasTables)
                {
                    _context.Database.Migrate();
                }
            }
        }
    }
}