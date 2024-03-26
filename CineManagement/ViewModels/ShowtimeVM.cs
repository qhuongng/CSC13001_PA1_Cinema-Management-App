using CineManagement.Model;
using CineManagement.Models;
using CineManagement.Services;
using CineManagement.View;
using CineManagement.Views;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;


namespace CineManagement.ViewModels
{
    class ShowtimeVM : ViewModelBase
    {
        private int _totalShowtime;
        private ObservableCollection<Projector> _showtimes;
        private Projector _selectedShowtime;
        public ProjectorService showtimeManage;

        public ICommand addCommand { get; }
        public ICommand updateCommand { get; }
        public ICommand deleteCommand { get; }
        public int TotalShowtime { get => _totalShowtime; set { _totalShowtime = value; OnPropertyChanged(nameof(TotalShowtime)); } }
        public ObservableCollection<Projector> Showtimes { get => _showtimes; set { _showtimes = value; OnPropertyChanged(nameof(Showtimes)); } }
        public Projector SelectedShowtime { get => _selectedShowtime; set { _selectedShowtime = value; OnPropertyChanged(nameof(SelectedShowtime)); } }

        public ShowtimeVM()
        {
            showtimeManage = new ProjectorService();
            _totalShowtime = 0;
            addCommand = new ViewModelCommand(executedAddCommand);
            updateCommand = new ViewModelCommand(executedUpdateCommand);
            deleteCommand = new ViewModelCommand(executedDeleteCommand);

            try
            {
                var sht = showtimeManage.getProjectors();
                _showtimes = new ObservableCollection<Projector>(sht);
                _totalShowtime = sht.Count;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void executedDeleteCommand(object obj)
        {
            if (_selectedShowtime != null)
            {
                try
                {
                    bool checkdel = showtimeManage.deleteProjectorById(_selectedShowtime.ProjectorId);

                    if (checkdel)
                    {
                        _showtimes.Remove(_selectedShowtime);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void executedUpdateCommand(object obj)
        {
            if (_selectedShowtime != null)
            {
                Projector temp = _selectedShowtime;

                var UpdateScreen = new UpdateShowtime(_selectedShowtime);
                UpdateScreen.ShowDialog();

                if (UpdateScreen.DialogResult == true)
                {
                    _selectedShowtime = UpdateScreen.updateShowtime;
                }
            }
        }

        private void executedAddCommand(object obj)
        {
            var AddScreen = new AddShowtime();
            AddScreen.ShowDialog();

            if (AddScreen.DialogResult == true)
            {
                List<Projector> temp = AddScreen.addShowtime;
                foreach (Projector prjt in temp)
                {
                    _showtimes.Add(prjt);
                }
            }

        }
    }
}
