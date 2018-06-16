using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Woof.WindowsEx.Controls {

    /// <summary>
    /// Windows-style spinner with optional percent display.
    /// </summary>
    public partial class Spinner : UserControl {

        /// <summary>
        /// Gets or sets percentage value, set -1 to hide percent display.
        /// </summary>
        public int Percent {
            get => _Percent; set {
                if (value < 0) {
                    PercentBox.Visibility = Visibility.Hidden;
                } else {
                    PercentBox.Visibility = Visibility.Visible;
                    PercentAmount.Text = value.ToString();
                }
                _Percent = value;
            }
        }

        /// <summary>
        /// Gets or sets the number of items processed. If <see cref="Total"/> value is set, percent display is shown.
        /// </summary>
        public int Done {
            get => _Done; set {
                if (_Total > 0) {
                    var percentValue = value / (double)_Total * 100;
                    PercentBox.Visibility = Visibility.Visible;
                    PercentAmount.Text = ((int)Math.Round(percentValue)).ToString();
                }
                else {
                    PercentBox.Visibility = Visibility.Hidden;
                    PercentAmount.Text = "E1/0";
                }
                _Done = value;
            }
        }

        /// <summary>
        /// Gets or sets total elements to process for percentage display.
        /// </summary>
        public int Total {
            get => _Total; set => _Total = value;
        }

        /// <summary>
        /// Initializes spinner with percentage diplay off.
        /// </summary>
        public Spinner() {
            InitializeComponent();
            Percent = -1;
            StartAnimation();
            SuspendAnimation();
        }

        #region Animation control

        /// <summary>
        /// Dot animation.
        /// </summary>
        Storyboard A0 => _A0 ?? (_A0 = (Storyboard)FindResource("A0"));

        /// <summary>
        /// Dot animation.
        /// </summary>
        Storyboard A1 => _A1 ?? (_A1 = (Storyboard)FindResource("A1"));

        /// <summary>
        /// Dot animation.
        /// </summary>
        Storyboard A2 => _A2 ?? (_A2 = (Storyboard)FindResource("A2"));

        /// <summary>
        /// Dot animation.
        /// </summary>
        Storyboard A3 => _A3 ?? (_A3 = (Storyboard)FindResource("A3"));

        /// <summary>
        /// Dot animation.
        /// </summary>
        Storyboard A4 => _A4 ?? (_A4 = (Storyboard)FindResource("A4"));

        /// <summary>
        /// Starts dots animation.
        /// </summary>
        void StartAnimation() {
            A0.Begin(this, true);
            A1.Begin(this, true);
            A2.Begin(this, true);
            A3.Begin(this, true);
            A4.Begin(this, true);
        }

        /// <summary>
        /// Suspends dots animation.
        /// </summary>
        void SuspendAnimation() {
            A0.Pause(this);
            A1.Pause(this);
            A2.Pause(this);
            A3.Pause(this);
            A4.Pause(this);
        }

        /// <summary>
        /// Resumes dots animation.
        /// </summary>
        void ResumeAnimation() {
            A0.Resume(this);
            A1.Resume(this);
            A2.Resume(this);
            A3.Resume(this);
            A4.Resume(this);
        }

        /// <summary>
        /// Suspends animation when the control is not visible.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e) {
            if (IsInitialized) {
                if (e.Property.GlobalIndex == _IsVisiblePropertyGlobalIndex) {
                    var value = (bool)e.NewValue;
                    if (value) ResumeAnimation(); else SuspendAnimation();
                }
                if (e.Property.GlobalIndex == _VisibilityPropertyGlobalIndex) {
                    var value = (Visibility)e.NewValue;
                    if (value == Visibility.Visible) ResumeAnimation(); else SuspendAnimation();
                }
            }
            base.OnPropertyChanged(e);
        }

        #endregion

        #region Private data

        int _Percent;
        int _Done;
        int _Total;
        Storyboard _A0;
        Storyboard _A1;
        Storyboard _A2;
        Storyboard _A3;
        Storyboard _A4;
        static int _IsVisiblePropertyGlobalIndex = IsVisibleProperty.GlobalIndex;
        static int _VisibilityPropertyGlobalIndex = VisibilityProperty.GlobalIndex;

        #endregion

    }

}