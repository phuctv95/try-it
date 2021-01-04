using System;
using TryDependencyInjectionWpf.Core;

namespace TryDependencyInjectionWpf
{
    public class BookModel : BaseNotifyPropertyChanged
    {
        public Guid Id { get; set; }

        public string Title
        {
            get => Get<string>();
            set => SetAndRaiseChangedNotify(value);
        }
        public bool Available { get; set; }
    }
}
