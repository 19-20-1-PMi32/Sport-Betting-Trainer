using Microsoft.EntityFrameworkCore;

namespace SBT.Database.Test
{
    public static class Utils
    {
        public static IUnitOfWork GetUnitOfWork(string name)
        {
            var options = new DbContextOptionsBuilder<DbContext>()
                .UseInMemoryDatabase(databaseName: name)
                .Options;

            var context = new DBContext(options);

            return new UnitOfWork(context);
        }
    }
}
