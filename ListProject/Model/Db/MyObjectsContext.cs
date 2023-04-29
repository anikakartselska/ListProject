using System.Data.Entity;


namespace ListProject.Model.Db
{
    class MyObjectsContext<TEntity>: DbContext where TEntity : class
    {
        public DbSet<TEntity> Entities { get; set; }
        public MyObjectsContext(): base(Properties.Settings.Default.DbConnect)
        { 
        
        }
    }
}
