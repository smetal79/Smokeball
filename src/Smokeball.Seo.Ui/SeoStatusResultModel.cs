using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Smokeball.Seo.Ui
{
    public class SeoStatusResultModel : INotifyPropertyChanged
    {
        private string rankings = string.Empty;
        private bool isFetching = false;

        public string Rankings
        {
            get { return this.rankings; }
            set
            {
                this.rankings = value;
                OnPropertyChanged(nameof(Rankings));
            }
        }

        public string? Uri { get; set; }

        public string? Keywords { get; set; }

        public bool IsFetching
        {
            get { return this.isFetching; }
            set
            {
                this.isFetching = value;
                OnPropertyChanged(nameof(IsFetching));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        internal void StartFetching() => IsFetching = true;
        internal void FetchingComplete() => IsFetching = false;
    }
}
