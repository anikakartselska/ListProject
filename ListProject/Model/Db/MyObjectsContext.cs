using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListProject.Model
{
    class MyObjectsContext<TEntity>: DbContext where TEntity : class
    {
        public DbSet<TEntity> Entities { get; set; }
        public MyObjectsContext(): base(Properties.Settings.Default.DbConnect)
        { 
        
        }
    }
}
