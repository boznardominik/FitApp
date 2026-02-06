using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace FitApp.Views
{
    public partial class VnosView : UserControl
    {
        public VnosView()
        {
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}

