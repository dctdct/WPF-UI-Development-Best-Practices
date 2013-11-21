using CommonLibrary.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Threading;

namespace CCDevShowcase.ViewModel.Samples
{
    public class SampleViewModel : ViewModelBase
    {
        #region Members

        private Random random = new Random();
        private int itemCount = 1;

        /// <summary>
        /// SelectedItemProperty source.
        /// </summary>
        private object selectedItem;

        #endregion

        #region Commands

        /// <summary>
        /// ClearSearchCommandProperty source.
        /// </summary>
        private DelegateCommand clearSearchCommand;

        /// <summary>
        /// SelectionChangedCommandProperty source.
        /// </summary>
        private DelegateCommand selectionChangedCommand;

        /// <summary>
        /// SelectionChangedInteractionCommand source.
        /// </summary>
        private DelegateCommand selectionChangedInteractionCommand;

        /// <summary>
        /// PointsProperty source.
        /// </summary>
        private ObservableCollection<PointViewModel> pointsSource = new ObservableCollection<PointViewModel>();

        /// <summary>
        /// SearchStringProperty source.
        /// </summary>
        private string searchString;

        #endregion

        #region Ctor

        public SampleViewModel()
        {
            RedrawPoints();

            DispatcherTimer timer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(3) };

            timer.Tick += (sender, e) =>
                {
                    RedrawPoints();
                };

            timer.Start();
        }

        #endregion

        #region Init/Cleanup



        #endregion

        #region OverrideMethods



        #endregion

        #region Methods

        /// <summary>
        /// ExecuteClearSearchCommand
        /// </summary>
        /// <param name="obj"></param>
        private void ExecuteClearSearchCommand(object obj)
        {
            SearchString = string.Empty;
        }

        /// <summary>
        /// ExecuteSelectionChangedCommand
        /// </summary>
        /// <param name="obj"></param>
        private void ExecuteSelectionChangedCommand(object obj)
        {
            Debug.WriteLine("ExecuteSelectionChangedCommand " + obj);
            Debug.WriteLine("");
        }

        /// <summary>
        /// ExecuteSelectionChangedInteractionCommand
        /// </summary>
        /// <param name="obj"></param>
        private void ExecuteSelectionChangedInteractionCommand(object obj)
        {
            Debug.WriteLine("ExecuteSelectionChangedInteractionCommand " + obj);
        }

        /// <summary>
        /// Readraw random points
        /// </summary>
        private void RedrawPoints()
        {
            if (Points.Count == 0)
            {
                for (int i = 0; i <= 100; i += 10)
                {
                    Points.Add(new PointViewModel() { X = i, Y = (int)(random.NextDouble() * 100), Name = "Bubble " + itemCount++ });
                }
            }
            else
            {
                Points.RemoveAt(0);

                for (int i = 0; i < Points.Count; i++)
                {
                    Points[i].X -= 10;
                }

                var point = new PointViewModel() { X = 100, Y = (int)(random.NextDouble() * 100), Name = "Bubble " + itemCount++ };

                Points.Add(point);
            }

            UpdateHighlighting();
        }

        /// <summary>
        /// Update SearchResult highlighting
        /// </summary>
        private void UpdateHighlighting()
        {
            foreach (var point in Points)
            {
                if (string.IsNullOrEmpty(SearchString))
                    point.IsSearchResult = null;
                else
                    point.IsSearchResult = point.Name.IndexOf(SearchString, StringComparison.InvariantCultureIgnoreCase) >= 0
                    || point.X.ToString().IndexOf(SearchString, StringComparison.InvariantCultureIgnoreCase) >= 0
                    || point.Y.ToString().IndexOf(SearchString, StringComparison.InvariantCultureIgnoreCase) >= 0;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// SelectedItemProperty gets or sets the SelectedItem.
        /// </summary>
        public object SelectedItem
        {
            get
            {
                return selectedItem;
            }

            set
            {
                if (value != this.selectedItem)
                {
                    this.selectedItem = value;
                    Debug.WriteLine("SelectedItem " + SelectedItem);
                    OnPropertyChanged("SelectedItem");
                }
            }
        }

        /// <summary>
        /// SelectionChangedCommandProperty gets or sets the SelectionChangedCommand.
        /// </summary>
        public DelegateCommand ClearSearchCommand
        {
            get
            {
                if (clearSearchCommand == null)
                    clearSearchCommand = new DelegateCommand(ExecuteClearSearchCommand);

                return clearSearchCommand;
            }
        }

        /// <summary>
        /// SelectionChangedCommandProperty gets or sets the SelectionChangedCommand.
        /// </summary>
        public DelegateCommand SelectionChangedCommand
        {
            get
            {
                if (selectionChangedCommand == null)
                    selectionChangedCommand = new DelegateCommand(ExecuteSelectionChangedCommand);

                return selectionChangedCommand;
            }
        }

        /// <summary>
        /// SelectionChangedInteractionCommand gets or sets the SelectionChangedCommand.
        /// </summary>
        public DelegateCommand SelectionChangedInteractionCommand
        {
            get
            {
                if (selectionChangedInteractionCommand == null)
                    selectionChangedInteractionCommand = new DelegateCommand(ExecuteSelectionChangedInteractionCommand);

                return selectionChangedInteractionCommand;
            }
        }

        /// <summary>
        /// PointsProperty gets or sets the Points.
        /// </summary>
        public ObservableCollection<PointViewModel> Points
        {
            get
            {
                return pointsSource;
            }

            set
            {
                if (value != this.pointsSource)
                {
                    this.pointsSource = value;
                    OnPropertyChanged("Points");
                }
            }
        }

        /// <summary>
        /// SearchStringProperty gets or sets the SearchString.
        /// </summary>
        public string SearchString
        {
            get
            {
                return searchString;
            }

            set
            {
                if (value != this.searchString)
                {
                    this.searchString = value;
                    UpdateHighlighting();
                    OnPropertyChanged("SearchString");
                }
            }
        }

        #endregion
    }
}
