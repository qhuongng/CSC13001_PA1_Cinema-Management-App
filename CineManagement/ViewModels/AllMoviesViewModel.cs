using CineManagement.Models;
using CineManagement.Services;
using Microsoft.IdentityModel.Tokens;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Windows.Input;

namespace CineManagement.ViewModels
{
    class AllMoviesViewModel : ViewModelBase
    {
        MovieService ms = new MovieService();

        public ObservableCollection<string> PageIndices { get; set; }
        public List<Movie> Movies { get; set; }
        public List<Movie> FilteredMovies { get; set; }
        public List<Director> Directors { get; set; }
        public List<Actor> Actors { get; set; }
        public List<Genre> Genres { get; set; }
        public List<string> ReleaseYears { get; set; }
        public string SearchTerm { get; set; }

        public List<Movie> MoviesInPage { get => _moviesInPage; set { _moviesInPage = value; OnPropertyChanged(nameof(MoviesInPage)); } }
        public Movie SelectedMovie { get => _selectedMovie; set { _selectedMovie = value; OnPropertyChanged(nameof(SelectedMovie)); } }
        public string SelectedPage {
            get => _selectedPage;
            set {
                _selectedPage = value;

                if (_selectedPage != null && PageIndices.Count > 0)
                {
                    int pageInd = int.Parse(_selectedPage);

                    if (_selectedPage.Equals(PageIndices.Last()))
                    {
                        MoviesInPage = FilteredMovies.GetRange(pageInd * 9 - 9, FilteredMovies.Count - 9 * (pageInd - 1));
                    }
                    else
                    {
                        MoviesInPage = FilteredMovies.GetRange(pageInd * 9 - 9, 9);
                    }
                }

                OnPropertyChanged(nameof(SelectedPage));
            }
        }
        public Director SelectedDirector
        {
            get => _selectedDirector;
            set {
                _selectedDirector = value;
                ApplyFilters();

                OnPropertyChanged(nameof(SelectedDirector));
            }
        }
        public string SelectedYear {
            get => _selectedYear;
            set {
                _selectedYear = value;
                ApplyFilters();

                OnPropertyChanged(nameof(SelectedYear));
            }
        }
        public Genre SelectedGenre
        {
            get => _selectedGenre;
            set
            {
                _selectedGenre = value;
                ApplyFilters();

                OnPropertyChanged(nameof(SelectedGenre));
            }
        }
        public bool IsSellingOnly
        {
            get => _isSellingOnly;
            set
            {
                _isSellingOnly = value;
                ApplyFilters();
                OnPropertyChanged(nameof(IsSellingOnly));
            }
        }

        public ICommand ResetFiltersCommand { get; }

        private Movie _selectedMovie;
        private string _selectedPage;
        private Director _selectedDirector;
        private string _selectedYear;
        private Genre _selectedGenre;
        private bool _isSellingOnly;
        private List<Movie> _moviesInPage;

        public AllMoviesViewModel(string searchTerm)
        {
            Movies = new List<Movie>();
            List<Movie> movies = ms.getMovies();

            if (!searchTerm.IsNullOrEmpty() || searchTerm.Length > 0)
            {
                SearchTerm = searchTerm.Trim();

                string normSearchTerm = NormalizeAndRemoveDiacritics(SearchTerm.ToLower());

                var filtered = new List<Movie>(
                        movies.Where(movie =>
                        NormalizeAndRemoveDiacritics(movie.MovieName.ToLower()).Contains(normSearchTerm) ||
                        movie.Actors.Any(actor =>
                            NormalizeAndRemoveDiacritics(actor.ActorName.ToLower()).Contains(normSearchTerm)) ||
                            NormalizeAndRemoveDiacritics(movie.Director.DirectorName.ToLower()).Contains(normSearchTerm)
                    )
                );

                Movies = filtered;
                FilteredMovies = filtered;
            }
            else
            {
                Movies = movies;
                FilteredMovies = movies;
            }

            ReleaseYears = new List<string>();
            Directors = new List<Director>();
            Genres = new List<Genre>();

            foreach (var movie in Movies)
            {
                if (!ReleaseYears.Any(y => y == movie.ReleaseYear.ToString()))
                {
                    ReleaseYears.Add(movie.ReleaseYear.ToString());
                }

                if (!Directors.Any(d => d.DirectorId == movie.DirectorId))
                {
                    Directors.Add(movie.Director);
                }

                foreach (var genre in movie.Genres)
                {
                    if (!Genres.Any(g => g.GenreName == genre.GenreName))
                    {
                        Genres.Add(genre);
                    }
                }
            }

            ReleaseYears = ReleaseYears.OrderByDescending(s => s).ToList();

            _isSellingOnly = false;

            _moviesInPage = new List<Movie>();
            PageIndices = new ObservableCollection<string>();
            Paginate();

            ResetFiltersCommand = new ViewModelCommand(ExecuteResetFiltersCommand, CanExecuteResetFiltersCommand);
        }

        private bool CanExecuteResetFiltersCommand(object obj)
        {
            if (SelectedDirector != null || !SelectedYear.IsNullOrEmpty() || SelectedGenre != null || IsSellingOnly == true)
            {
                return true;
            }

            return false;
        }

        private void ExecuteResetFiltersCommand(object obj)
        {
            SelectedDirector = null;
            SelectedYear = null;
            SelectedGenre = null;
            IsSellingOnly = false;
        }

        public void ApplyFilters()
        {
            FilteredMovies = Movies;

            if (SelectedDirector != null)
            {
                FilteredMovies = FilteredMovies.Where(movie => movie.Director == SelectedDirector).ToList();
            }

            if (!SelectedYear.IsNullOrEmpty())
            {
                int year = int.Parse(SelectedYear);
                FilteredMovies = FilteredMovies.Where(movie => movie.ReleaseYear == year).ToList();
            }

            if (SelectedGenre != null)
            {
                FilteredMovies = FilteredMovies.Where(movie => movie.Genres.Any(genre => genre == SelectedGenre)).ToList();
            }

            if (IsSellingOnly)
            {
                FilteredMovies = FilteredMovies.Where(movie => movie.MovieInfo.IsSelling == true).ToList();
            }

            Paginate();
        }

        private void Paginate()
        {
            PageIndices.Clear();
            _moviesInPage.Clear();

            int n = FilteredMovies.Count / 9;

            if (FilteredMovies.Count % 9 > 0)
            {
                n++;
            }

            if (FilteredMovies.Count >= 9)
            {
                for (int i = 0; i < n; i++)
                {
                    PageIndices.Add($"{i + 1}");
                }

                MoviesInPage = FilteredMovies.GetRange(0, 9);
                SelectedPage = PageIndices[0];
            }
            else
            {
                MoviesInPage = FilteredMovies.GetRange(0, FilteredMovies.Count);
            }
        }

        private string NormalizeAndRemoveDiacritics(string input)
        {
            string normalizedString = input.Normalize(NormalizationForm.FormKD);
            var builder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    builder.Append(c);
                }
            }

            return builder.ToString().Normalize(NormalizationForm.FormKC);
        }
    }
}
