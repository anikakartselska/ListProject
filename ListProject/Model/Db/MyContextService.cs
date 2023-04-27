using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ListProject.Model
{
    public class MyContextService
    {
        public List<dynamic> GetEntitiesListFromDatabaseByType(Type entityType)
        {
            Type myObjectContextType = typeof(MyObjectsContext<>).MakeGenericType(entityType);

            dynamic myObjectContext = Activator.CreateInstance(myObjectContextType);
            PropertyInfo entitiesProperty = myObjectContextType.GetProperty("Entities");
            object genericDbSet = entitiesProperty?.GetValue(myObjectContext, null);
            MethodInfo toListMethod = typeof(Enumerable).GetMethod("ToList")?.MakeGenericMethod(entityType);
            object entitiesList = toListMethod?.Invoke(null, new[] { genericDbSet });
            List<dynamic> dynamicEntitiesList = ((IEnumerable)entitiesList).Cast<dynamic>().ToList();
            return dynamicEntitiesList;
        }
    }
}