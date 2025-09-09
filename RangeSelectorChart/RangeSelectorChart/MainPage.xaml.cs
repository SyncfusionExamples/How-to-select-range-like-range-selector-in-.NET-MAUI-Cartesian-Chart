using System.Collections.ObjectModel;
using System.ComponentModel;

namespace RangeSelectorChart
{
    public partial class MainPage : ContentPage
    {
        private ViewModel ViewModel { get; set; }
        public MainPage()
        {
            InitializeComponent();
            ViewModel = new ViewModel();
            BindingContext = ViewModel;
        } 
    }

    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private double rangeStart = 6;
        private double rangeEnd = 16;
        private ObservableCollection<Brush> brushes = new();
        private double totalDataUsage;

        public ObservableCollection<ChartDataModel> Items { get; set; }

        public ObservableCollection<Brush> Brushes
        {
            get { return brushes; }
            set
            {
                brushes = value;
                OnPropertyChanged(nameof(Brushes));
            }
        }

        public double RangeStart
        {
            get { return rangeStart; }
            set
            {
                if (rangeStart != value)
                {
                    rangeStart = value;
                    UpdatePaletteBrushes();
                    OnPropertyChanged(nameof(RangeStart));
                }
            }
        }

        public double RangeEnd
        {
            get { return rangeEnd; }
            set
            {
                if (rangeEnd != value)
                {
                    rangeEnd = value;
                    UpdatePaletteBrushes();
                    OnPropertyChanged(nameof(RangeEnd));
                }
            }
        }


        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void UpdatePaletteBrushes()
        {
            ObservableCollection<Brush> PaletteBrushes = new();
            foreach (ChartDataModel item in Items)
            {
                if (item.Day >= rangeStart && item.Day <= rangeEnd)
                {
                    PaletteBrushes.Add(new SolidColorBrush(Color.FromArgb("#F69522")));
                }
                else
                {
                    PaletteBrushes.Add(new SolidColorBrush(Color.FromArgb("#303B7D")));
                }
            }
            Brushes = PaletteBrushes;
            UpdateTotalDataUsage();
        }

        private void UpdateTotalDataUsage()
        {
            double total = 0;
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].Day >= rangeStart && Items[i].Day <= rangeEnd)
                {
                    total += Items[i].Value;
                }
            }
        }

        public ViewModel()
        {
            Items = new ObservableCollection<ChartDataModel>()
        {
            new ChartDataModel(1, 0.2),
            new ChartDataModel(2, 0.3),
            new ChartDataModel(3, 0.4),
            new ChartDataModel(4, 0.6),
            new ChartDataModel(5, 0.8),
            new ChartDataModel(6, 1.2),
            new ChartDataModel(7, 1.6),
            new ChartDataModel(8, 2.4),
            new ChartDataModel(9, 3.2),
            new ChartDataModel(10, 4.8),
            new ChartDataModel(11, 6.4),
            new ChartDataModel(12, 9.6),
            new ChartDataModel(13, 12.8),
            new ChartDataModel(14, 16.0),
            new ChartDataModel(15, 22.0),
            new ChartDataModel(16, 25.6),
            new ChartDataModel(17, 20.0),
            new ChartDataModel(18, 14.5),
            new ChartDataModel(19, 12.8),
            new ChartDataModel(20, 10.0),
            new ChartDataModel(21, 6.6),
            new ChartDataModel(22, 5.0),
            new ChartDataModel(23, 3.2),
            new ChartDataModel(24, 3.2),
            new ChartDataModel(25, 1.6),
            new ChartDataModel(26, 1.6),
            new ChartDataModel(27, 0.8),
            new ChartDataModel(28, 0.8),
            new ChartDataModel(29, 0.4),
            new ChartDataModel(30, 0.2)
        };
            UpdatePaletteBrushes();
        }
    }

    public class ChartDataModel
    {
        public int Day { get; set; }
        public double Value { get; set; }

        public ChartDataModel(int day, double value)
        {
            Day = day;
            Value = value;
        }
    }
}
