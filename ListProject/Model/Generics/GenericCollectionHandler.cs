using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ListProject.ViewModel
{
    public class GenericCollectionHandler<T>:IEnumerable<T>
    {
        public GenericCollectionHandler(ObservableCollection<T> collection)
        {
            Collection = collection;
        }

        public ObservableCollection<T> Collection { get; set; }
        public IEnumerator<T> GetEnumerator()
        {
            return Collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}