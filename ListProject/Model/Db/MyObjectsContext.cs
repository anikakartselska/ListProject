using System.Data.Entity;
using ListProject.Model.Entities;

namespace ListProject.Model.Db
{
    class MyObjectsContext<TEntity>: DbContext where TEntity : Entity
    {
        public DbSet<TEntity> Entities { get; set; }
        public MyObjectsContext(): base(Properties.Settings.Default.DbConnect)
        { 
        
        }
    }
}
