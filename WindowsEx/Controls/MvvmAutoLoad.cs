using System.Windows;
using System.Windows.Controls;

namespace Woof.WindowsEx.Controls {

    /// <summary>
    /// A user control that automatically loads the view when it first becomes visible
    /// if its DataContext implements <see cref="IGetAsync"/> interface.
    /// </summary>
    public class MvvmAutoLoad : UserControl {

        protected override async void OnPropertyChanged(DependencyPropertyChangedEventArgs e) {
            base.OnPropertyChanged(e);
            if (e.Property == IsVisibleProperty) {
                if (DataContext is IGetAsync viewModel && (bool)e.NewValue && !viewModel.IsLoaded) await viewModel.GetAsync();
            }
        }

    }

}