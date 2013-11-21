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
    public class PointViewModel : ViewModelBase
    {
        #region Memebrs

        /// <summary>
        /// XProperty source.
        /// </summary>
        private double x;

        /// <summary>
        /// YProperty source.
        /// </summary>
        private double y;

        /// <summary>
        /// NameProperty source.
        /// </summary>
        private string name;

        /// <summary>
        /// IsSelectedProperty source.
        /// </summary>
        private bool isSelected;

        /// <summary>
        /// IsSearchResultProperty source.
        /// </summary>
        private bool? isSearchResult;

        #endregion

        #region overrideMethods

        /// <summary>
        /// Override to string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}, {1}", X, Y);
        }

        #endregion

        #region Properties

        /// <summary>
        /// XProperty gets or sets the X.
        /// </summary>
        public double X
        {
            get
            {
                return x;
            }

            set
            {
                if (value != this.x)
                {
                    this.x = value;
                    OnPropertyChanged("X");
                }
            }
        }

        /// <summary>
        /// YProperty gets or sets the Y.
        /// </summary>
        public double Y
        {
            get
            {
                return y;
            }

            set
            {
                if (value != this.y)
                {
                    this.y = value;
                    OnPropertyChanged("Y");
                }
            }
        }

        /// <summary>
        /// NameProperty gets or sets the Name.
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                if (value != this.name)
                {
                    this.name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        /// <summary>
        /// IsSelectedProperty gets or sets the IsSelected.
        /// </summary>
        public bool IsSelected
        {
            get
            {
                return isSelected;
            }

            set
            {
                if (value != this.isSelected)
                {
                    this.isSelected = value;
                    OnPropertyChanged("IsSelected");
                }
            }
        }

        /// <summary>
        /// IsSearchResultProperty gets or sets the IsSearchResult.
        /// </summary>
        public bool? IsSearchResult
        {
            get
            {
                return isSearchResult;
            }

            set
            {
                if (value != this.isSearchResult)
                {
                    this.isSearchResult = value;
                    OnPropertyChanged("IsSearchResult");
                }
            }
        }

        #endregion
    }
}
