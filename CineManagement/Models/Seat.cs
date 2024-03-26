using System.ComponentModel;

namespace CineManagement.Models
{
    class Seat : INotifyPropertyChanged
    {
        public string SeatId { get; set; }
        public bool IsEnabled { get => _isEnabled; set { _isEnabled = value; OnPropertyChanged(nameof(IsEnabled)); } }

        private bool _isEnabled;

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Seat()
        {
            SeatId = "";
            _isEnabled = true;
        }

        public Seat(string id)
        {
            SeatId = id;
            _isEnabled = true;
        }
    }
}
