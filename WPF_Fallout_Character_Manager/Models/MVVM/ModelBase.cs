using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Fallout_Character_Manager.Models.MVVM
{
    interface ISerializable<TDto>
    {
        TDto ToDto();
        void FromDto(TDto dto, bool versionMismatch = false);
    }

    public class ModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        protected void Update<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                OnPropertyChanged(propertyName);
            }
        }

        protected void Update<T>(T oldValue, T newValue, Action<T> setter, [CallerMemberName] string? propertyName = null)
        {
            if(!EqualityComparer<T>.Default.Equals(oldValue, newValue))
            {
                setter(newValue);
                OnPropertyChanged(propertyName);
            }
        }
    }
}
