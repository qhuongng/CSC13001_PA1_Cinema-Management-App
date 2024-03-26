using CineManagement.Models;
using CineManagement.Services;
using System.Windows;
using System.Windows.Input;

namespace CineManagement.ViewModels
{
    internal class ShowtimeUpdateViewModel : ViewModelBase
    {
        private DateTime _showtime;
        private string _errorMessage;
        private string _successMessage;

        public DateTime Showtime { get => _showtime; set { _showtime = value; OnPropertyChanged(nameof(Showtime)); } }
        public string ErrorMessage { get => _errorMessage; set { _errorMessage = value; OnPropertyChanged(nameof(ErrorMessage)); } }
        public string SuccessMessage { get => _successMessage; set { _successMessage = value; OnPropertyChanged(nameof(SuccessMessage)); } }

        public ICommand UpdateCommand { get; }

        public Projector showtime;
        public ProjectorService showtimeService;
        public Window window;

        public ShowtimeUpdateViewModel(Projector currentProjector, Window currentWindow)
        {
            showtime = currentProjector;
            window = currentWindow;
            showtimeService = new ProjectorService();
            _showtime = showtime.ProjectorInfo;
            _errorMessage = "";
            _successMessage = "";

            UpdateCommand = new ViewModelCommand(ExecutedUpdateCommand);
        }

        private void ExecutedUpdateCommand(object obj)
        {
            if (Showtime < DateTime.Now)
            {
                ErrorMessage = "Giá trị không hợp lệ!";
                SuccessMessage = "";
            }
            else if (_showtime == showtime.ProjectorInfo)
            {
                ErrorMessage = "Không có thông tin nào thay đổi!";
                SuccessMessage = "";
            }
            else
            {
                showtime.ProjectorInfo = _showtime;
                try
                {
                    bool checkUpdate = showtimeService.updateProjector(showtime);
                    if (checkUpdate)
                    {
                        SuccessMessage = "Cập nhật thành công.";
                        window.DialogResult = true;
                        window.Close();
                    }
                }
                catch (Exception ex)
                {
                    ErrorMessage = "" + ex.Message;
                    SuccessMessage = "";
                }


            }
        }
    }
}
