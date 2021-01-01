using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TryMvvm.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private Dictionary<string, object> _properties { get; } = new Dictionary<string, object>();

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        protected T Get<T>([CallerMemberName] string? name = null)
        {
            return name != null && _properties.TryGetValue(name, out object? value)
                ? (T)value
                : default!;
        }
        protected void SetAndRaiseChangedNotify<T>(T value, [CallerMemberName] string? name = null)
        {
            if (name == null || Equals(value, Get<T>(name)))
            {
                return;
            }
            _properties[name] = value!;
            OnPropertyChanged(name);
        }
    }
}
