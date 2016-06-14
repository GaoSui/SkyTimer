using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SkyTimer.MVVM
{
    public class ObservableObject : INotifyPropertyChanged
    {
        protected bool Set<TValue>(ref TValue field, TValue value, [CallerMemberName] string propName = null)
        {
            if (EqualityComparer<TValue>.Default.Equals(field, value))
            {
                return false;
            }
            else
            {
                field = value;
                OnPropertyChanged(propName);
                return true;
            }
        }

        //protected void Set<TValue>(ref TValue field, TValue value, [CallerMemberName] string propName = null)
        //{
        //    field = value;
        //    OnPropertyChanged(propName);
        //}

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName]string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
