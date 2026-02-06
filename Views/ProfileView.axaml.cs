using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace FitApp.Views;

public partial class ProfileView : UserControl
{
    public ProfileView()
    {
        InitializeComponent();
    }
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

}