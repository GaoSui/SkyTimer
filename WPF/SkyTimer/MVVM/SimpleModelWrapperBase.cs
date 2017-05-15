using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Linq;

namespace SkyTimer.MVVM
{
    public class SimpleModelWrapperBase<T> : ObservableObject
    {
        public SimpleModelWrapperBase(T model)
        {
            Model = model;
        }

        public T Model { get; private set; }

        protected bool Set<TValue>(TValue newValue, [CallerMemberName]string propName = null)
        {
            var info = typeof(T).GetProperty(propName);
            var oldValue = (TValue)info.GetValue(Model);

            if (!EqualityComparer<TValue>.Default.Equals(oldValue, newValue))
            {
                info.SetValue(Model, newValue);
                OnPropertyChanged(propName);

                return true;
            }
            return false;
        }

        protected void RegisterCollection<TModel>(ObservableCollection<TModel> collection, List<TModel> list)
        {
            collection.CollectionChanged += (sender, e) =>
            {
                list.Clear();
                list.AddRange(collection);
            };
        }

        protected void RegisterCollection<TWrapper, TModel>(ObservableCollection<TWrapper> collection, List<TModel> list)
            where TWrapper : SimpleModelWrapperBase<TModel>
        {
            collection.CollectionChanged += (sender, e) =>
            {
                list.Clear();
                list.AddRange(collection.Select(wrapper => wrapper.Model));
            };
        }
    }
}
