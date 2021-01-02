using TryMvvm.Base;

namespace TryMvvm.Model
{
    public class FileModel : BaseNotifyPropertyChanged
    {
        public string FileName { get; set; } = string.Empty;
        public bool Selected { get; set; }
        public string Status
        {
            get => Get<string>();
            set => SetAndRaiseChangedNotify(value);
        }
    }
}
