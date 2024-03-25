using CineManagement.Models;
using CineManagement.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineManagement.ViewModels
{
    class MovieAddViewModel: ViewModelBase
    {
        private string _movieName;
        private int _duration;
        private AgeRating _selectedAge;
        private double _imdbRating;
        private int _releaseYear;
        private bool _checkShow;
        private string _errorMessage;
        private string _successMessage;
        private Director _selectedDirector;
        private int _soldTicket;
        private int _dailyShowTime;
        private int _weeklyShowTime;
        private int _monthlyShowTime;
        private long _ticketRevenue;

        public ObservableCollection<AgeRating> AgeRating { get; set; }
        public ObservableCollection<bool> IsShow { get; set; }
        public ObservableCollection<Director> Directors { get; set; }
        public MovieService movieManage;
        public AgeRatingService ageManage;
        public DirectorService directorManage;
    }
}
