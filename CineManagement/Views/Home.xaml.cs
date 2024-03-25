using CineManagement.ViewModels;
using CineManagement.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Controls.Primitives;

namespace CineManagement.Views
{
    public partial class Home : UserControl
    {
        public Home()
        {
            InitializeComponent();
            DataContext = new HomeViewModel();
        }

        private void movieList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView lv = sender as ListView;
            MainWindow mw = (MainWindow) Window.GetWindow(this);
            MainWindowViewModel vm = (MainWindowViewModel) mw.DataContext;
            Movie selected = (Movie) lv.SelectedItem;
            User? currentUser = null;

            if (vm.CurrentUser != null)
            {
                currentUser = vm.CurrentUser;
            }

            mw.MovieDetailsView.DataContext = new MovieDetailsViewModel(selected, currentUser);
            mw.MovieDetailsView.SeatChart.UnselectAll();
            mw.HideAllExcept(mw.Root, mw.MovieDetailsView);
        }

        private void HandleCarouselScrollLeft(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            ListView lv = FindListView((DockPanel)b.Parent);

            SmoothScrollingWrapPanel wrapPanel = FindChild<SmoothScrollingWrapPanel>(lv);

            if (wrapPanel != null)
            {
                double newOffset = Math.Max(0, wrapPanel.HorizontalOffset - 250);

                if (newOffset != wrapPanel.HorizontalOffset)
                {
                    wrapPanel.AnimateScroll(newOffset, 0.2);
                }

                e.Handled = true;
            }
        }

        private void HandleCarouselScrollRight(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            ListView lv = FindListView((DockPanel)b.Parent);

            SmoothScrollingWrapPanel wrapPanel = FindChild<SmoothScrollingWrapPanel>(lv);

            if (wrapPanel != null)
            {
                double maxOffset = CalculateMaxOffset(wrapPanel, lv);
                double newOffset = Math.Min(maxOffset, wrapPanel.HorizontalOffset + 250);

                if (wrapPanel.HorizontalOffset != newOffset)
                {
                    wrapPanel.AnimateScroll(newOffset, 0.2); // Change the duration here
                }

                e.Handled = true;
            }
        }

        private double CalculateMaxOffset(SmoothScrollingWrapPanel wrapPanel, ListView lv)
        {
            double totalWidth = 0;

            foreach (UIElement child in wrapPanel.Children)
            {
                totalWidth += child.RenderSize.Width + 10;
            }

            double viewportWidth = lv.ActualWidth - lv.Padding.Left - lv.Padding.Right;

            return Math.Abs(totalWidth - viewportWidth);
        }

        private ListView FindListView(DockPanel panel)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(panel); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(panel, i);

                if (child is ListView listView)
                {
                    return listView;
                }
            }

            return null;
        }

        public static T FindChild<T>(DependencyObject parent) where T : DependencyObject
        {
            if (parent == null) return null;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child != null && child is T)
                {
                    return (T)child;
                }

                var childOfChild = FindChild<T>(child);

                if (childOfChild != null)
                {
                    return childOfChild;
                }
            }

            return null;
        }

        private void HandleFail(object sender, ExceptionRoutedEventArgs e)
        {
            MessageBox.Show("Trailer video failed: " + e.ErrorException);
        }

        private void ElementPopup_Opened(object sender, EventArgs e)
        {
            Popup popup = sender as Popup;
            MediaElement trailer = FindChild<MediaElement>(popup.Child);
            trailer.Source = new Uri("pack://siteoforigin:,,,/Clips/trailer.mp4");

            trailer.Play();
        }

        private void ElementPopup_Closed(object sender, EventArgs e)
        {
            Popup popup = sender as Popup;
            MediaElement trailer = FindChild<MediaElement>(popup.Child);

            trailer.Stop();
        }
    }

    public class SmoothScrollingWrapPanel : WrapPanel
    {
        public double HorizontalOffset
        {
            get => (double)GetValue(HorizontalOffsetProperty);
            set => SetValue(HorizontalOffsetProperty, value);
        }

        public static readonly DependencyProperty HorizontalOffsetProperty =
            DependencyProperty.Register("HorizontalOffset", typeof(double), typeof(SmoothScrollingWrapPanel),
                new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        protected override Size MeasureOverride(Size availableSize)
        {
            var size = base.MeasureOverride(availableSize);
            ScrollToHorizontalOffset(HorizontalOffset);
            return size;
        }

        private void ScrollToHorizontalOffset(double offset)
        {
            foreach (UIElement child in InternalChildren)
            {
                var trans = child.RenderTransform as TranslateTransform ?? new TranslateTransform();
                trans.X = -offset;
                child.RenderTransform = trans;
            }
        }

        public void AnimateScroll(double offset, double durationSeconds)
        {
            var animation = new DoubleAnimation();
            animation.To = offset;
            animation.Duration = TimeSpan.FromSeconds(durationSeconds);

            BeginAnimation(HorizontalOffsetProperty, animation);
        }
    }
}
