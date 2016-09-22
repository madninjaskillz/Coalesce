using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace coalesce
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }

        }

        public bool Set<T>(ref T input, T newValue, [CallerMemberName] string name = "")
        {
            if ((input == null && newValue != null) || (input != null && newValue == null) || (!input.Equals(newValue)))
            {
                input = newValue;
                RaisePropertyChanged(name);
                return true;
            }

            return false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
    }
}
